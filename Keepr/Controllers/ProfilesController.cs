using System.Collections.Generic;
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
    public ActionResult<Profile> Get(string profileId)
    {
      try
      {
        Profile profile = _profilesService.Get(profileId);
        return Ok(profile);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
       [HttpGet("{profileId}/vaults")]
        public ActionResult<List<Vault>> GetVaultsByProfile(string profileId)
    {
      try
      {
        var profile = _profilesService.GetVaultsByProfile(profileId);
        return Ok(profile);
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    }
}