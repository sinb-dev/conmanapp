using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Docker.DotNet;
using Docker.DotNet.Models;
using Conman.Models;

namespace Conman.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DockerController : ControllerBase
    {
        [HttpGet("{token}")]
        public async Task<IActionResult> Get(string token)
        {
            using (var context = new ConmanContext()) {
                var containers = context.Containers
                    .Where(c => c.Token.TokenId.Equals(token));
                return Ok(containers.ToList());
            }
        }
        

        [HttpGet("{token}/direct")]
        public async Task<IActionResult> GetDirect(string token)
        {
            DockerClient client = new DockerClientConfiguration()
            .CreateClient();
            IList<ContainerListResponse> containers = await client.Containers.ListContainersAsync(
                new ContainersListParameters(){
                    Limit = 10,
                });
            
             return Ok(containers.ToArray<ContainerListResponse>());
        }
    }
}
