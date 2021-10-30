using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {

      private readonly VaultsRepository _vaultsRepository;
      private readonly VaultKeepsRepository _vaultKeepsRepository;

      public VaultsService(VaultsRepository vaultsRepository, VaultKeepsRepository vaultKeepsRepository)
      {
          _vaultsRepository = vaultsRepository;
          _vaultKeepsRepository = vaultKeepsRepository;
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

    public Vault Post(Vault newVaultKeep)
    {
      return _vaultsRepository.Create(newVaultKeep);
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

    public List<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
    {
      return _vaultKeepsRepository.GetKeepsByVaultId(vaultId);
    }
  }
}