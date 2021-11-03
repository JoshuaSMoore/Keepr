using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class ProfilesService
  {
      private readonly ProfilesRepository _profilesRepository;

      public ProfilesService(ProfilesRepository profilesRepository)
      {
          _profilesRepository = profilesRepository;
      }

public List<Keep> GetKeepByProfile(string profileId)
    {
      return _profilesRepository.GetKeepByProfile(profileId);
    }
public Profile Get(string profileId)
    {
      return _profilesRepository.Get(profileId);
    }

    public List<Vault> GetVaultsByProfile(string profileId, string userId)
    {
      if(profileId != userId){
        var foundVaults = _profilesRepository.GetVaultsByProfile(profileId);
        return foundVaults;
      }
      else{
        return _profilesRepository.GetVaultsByUser(profileId);
      }
    }
  }
}