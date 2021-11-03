using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
      [ApiController]
        [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
           private readonly ProfilesService _profilesService;
           private readonly KeepsService _keepsService;

           private readonly VaultsService _vaultsService;
        public ProfilesController(ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService)
            {
                _profilesService = profilesService;
                _keepsService = keepsService;
                _vaultsService = vaultsService;
            }
            [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfile(string profileId)
    {
      try
      {
        var profile = _profilesService.GetKeepByProfile(profileId);
        return Ok(profile);
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
       [HttpGet("{profileId}")]
    public async Task<ActionResult<Profile>> GetAsync(string profileId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Profile profile = _profilesService.Get(profileId);
        return Ok(profile);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
       [HttpGet("{profileId}/vaults")]
      async  public Task<ActionResult<List<Vault>>> GetVaultsByProfile(string profileId, string userId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        var profile = _profilesService.GetVaultsByProfile(profileId, userInfo?.Id);
        return Ok(profile);
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    }
}