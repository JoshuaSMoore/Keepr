using System;
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
      Profile profile = _profilesRepository.Get(profileId);
      if (profile == null)
      {
        throw new Exception("Profile not found");
      }
      return profile;
    }
    public List<Vault> GetVaultsByProfile(string profileId)
    {
      return _profilesRepository.GetVaultsByProfile(profileId);
    }
  }
}