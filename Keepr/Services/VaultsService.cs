using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {

      private readonly VaultsRepository _vaultsRepository;

      public VaultsService(VaultsRepository vaultsRepository)
      {
          _vaultsRepository = vaultsRepository;
      }
    public Vault GetById(int vaultId, string id)
    {
      Vault foundVault = _vaultsRepository.Get(vaultId);
      if (foundVault == null)
      {
        throw new Exception("Invalid Id");
      }
      if (foundVault.IsPrivate == true && foundVault.CreatorId != id)
      {
        throw new Exception("This aint it dude");
      }
      return foundVault;
    }

    public List<Vault> GetAll()
    {
     return _vaultsRepository.Get();
    }

    public Vault Post(Vault vaultData)
    {
      return _vaultsRepository.Create(vaultData);
    }

    public void RemoveVault(int vaultId, string userId)
    {
      Vault foundVault = _vaultsRepository.Get(vaultId);
      if(foundVault.CreatorId != userId)
      {
        throw new Exception("You are not the creator of this vault");
      }
      _vaultsRepository.Delete(vaultId);
    }

    public Vault Edit(Vault editedVault)
    {
        Vault foundVault = GetById(editedVault.Id, editedVault.CreatorId);
        if(foundVault.CreatorId != editedVault.CreatorId)
        {
          throw new Exception("You are not the creator of this vault");
        }
        foundVault.Name = editedVault.Name ?? foundVault.Name;
        foundVault.Description = editedVault.Description ?? foundVault.Description;
        foundVault.IsPrivate = editedVault.IsPrivate;
        return _vaultsRepository.Edit(foundVault);
    }
  }
}