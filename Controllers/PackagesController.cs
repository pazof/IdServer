using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NuGet.Packaging;

namespace nuget_host.Controllers
{
    public class PackagesController : Controller
    {
        private ILogger<PackagesController> logger;
        private IDataProtector protector;

        public PackagesController(ILoggerFactory loggerFactory, IDataProtectionProvider provider)
        {
            logger = loggerFactory.CreateLogger<PackagesController>();
            protector = provider.CreateProtector("Packages.v1");
        }

        [HttpPut("packages/{*spec}")]
        public IActionResult Put(string spec)
        {
            string path = null;
            
            if (string.IsNullOrEmpty(spec))
            {
                var clientVersionId = Request.Headers["X-NuGet-Client-Version"];
                var apiKey = Request.Headers["X-NuGet-ApiKey"];
                ViewData["nuget client "] = "nuget {clientVersionId}";
                
                var clearkey = protector.Unprotect(apiKey);
                if (clearkey!= Startup.RootApiKeySecret)
                    return Unauthorized();

                foreach (var file in Request.Form.Files)
                {
                    string initpath = "package.nupkg";
                    using (FileStream fw = new FileStream(initpath, FileMode.Create))
                    {
                        file.CopyTo(fw);
                    }

                    using (FileStream fw = new FileStream(initpath, FileMode.Open))
                    {
                        var archive = new System.IO.Compression.ZipArchive(fw);
                        foreach (var filename in archive.GetFiles())
                        {
                            if (filename.EndsWith(".nuspec"))
                            {
                                // var entry = archive.GetEntry(filename);
                                var specstr = archive.OpenFile(filename);
                                NuspecReader reader = new NuspecReader(specstr);

                                string pkgdesc = reader.GetDescription();
                                string pkgid = reader.GetId();
                                var version = reader.GetVersion();


                                path = Path.Combine(Startup.SourceDir,
                                Path.Combine(pkgid,
                                Path.Combine(version.ToFullString(),
                                     $"{pkgid}-{version}.nupkg")));
                                var source = new FileInfo(initpath);
                                var dest = new FileInfo(path);
                                var destdir = new DirectoryInfo(dest.DirectoryName);
                                if (dest.Exists)
                                    return BadRequest(new {error = "existant"});

                                if (!destdir.Exists) destdir.Create();
                                source.MoveTo(path);
                            }

                        }

                    }



                }
            }
            else
            {
                ViewData["spec"] = spec;
                // TODO Assert valid sem ver spec
                var filelst = new DirectoryInfo(Startup.SourceDir);
                var fi = new FileInfo(spec);
                var lst = filelst.GetFiles(fi.Name + "*.nupkg");
                ViewData["lst"] = lst.Select(entry => entry.Name);
            }
            return Ok(ViewData);
        }

        [HttpGet("Packages/{spec}")]
        public IActionResult Index(string spec)
        {
            if (string.IsNullOrEmpty(spec))
            {
                ViewData["warn"] = "no spec";
                /*
                usr/lib/mono/msbuild/Current/bin/NuGet.targets(128,5): error : Failed to retrieve information about 'Microsoft.VisualStudio.Web.CodeGeneration.Tools' from remote source 'http://localhost:5000/Packages/FindPackagesById()?id='Microsoft.VisualStudio.Web.CodeGeneration.Tools'&semVerLevel=2.0.0'. [/home/paul/workspace/nuget-host/nuget-host.csproj]
                
                
                
                */
            }
            else
            {
                ViewData["spec"] = spec;
                // TODO Assert valid sem ver spec
                var filelst = new DirectoryInfo(Startup.SourceDir);
                var fi = new FileInfo(spec);
                var lst = filelst.GetDirectories(spec);
                ViewData["lst"] = lst.Select(entry => entry.Name);
            }
            return Ok(ViewData);
        }

        [Authorize]
        [HttpGet("api/get-key/{*apikey}")]
        public IActionResult GetApiKey(string apiKey)
        {
            return Ok(protector.Protect(apiKey));
        }
    }
}