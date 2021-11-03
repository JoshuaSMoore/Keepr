using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;

        public VaultsController(VaultsService vaultsService)
        {
            _vaultsService = vaultsService;
        }
        [HttpGet]

        public ActionResult<List<Vault>> GetAll()
        {
            try
            {
                return Ok(_vaultsService.GetAll());
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("{vaultId}")]
        public async Task<ActionResult<Vault>> GetById(int vaultId)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_vaultsService.GetById(vaultId, userInfo?.Id));
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Vault>> Post([FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                vaultData.CreatorId = userInfo.Id;
                Vault createdVault = _vaultsService.Post(vaultData);
                createdVault.Creator = userInfo;
                return Ok(createdVault);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("{vaultId}")]
        public async Task<ActionResult<string>> Delete(int vaultId)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _vaultsService.RemoveVault(vaultId, userInfo?.Id);
                return Ok("Vault Deleted");
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize]
        [HttpPut("{vaultId}")]
        public async Task<ActionResult<Vault>> Edit([FromBody] Vault vaultData, int vaultId)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                vaultData.CreatorId = userInfo.Id;
                vaultData.Id = vaultId;
                return Ok(_vaultsService.Edit(vaultData));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
    }
 [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<VaultKeepViewModel>>> GetKeepsByVaultId(int vaultId)
    {
      try
      {
           Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<VaultKeepViewModel> keeps = _vaultsService.GetKeepsByVaultId(vaultId, userInfo?.Id);
        return Ok(keeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    }
}