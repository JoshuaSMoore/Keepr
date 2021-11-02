using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vaultKeepsRepository;
    private readonly VaultsRepository _vaultsRepository;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsRepository vaultsRepository)
    {
      _vaultKeepsRepository = vaultKeepsRepository;
      _vaultsRepository = vaultsRepository;
    }
    public VaultKeep Post(VaultKeep vaultKeepData)
    {
      return _vaultKeepsRepository.Create(vaultKeepData);
    }

    public VaultKeep GetById(int id)
    {
    VaultKeep foundVaultKeep =  _vaultKeepsRepository.GetById(id);
    if(foundVaultKeep == null)
    {
      throw new Exception("Invalid Id");
    }
    return foundVaultKeep;
    }
      public VaultKeep Get(int vaultKeepsId, string id)
    {
      VaultKeep foundVault = _vaultKeepsRepository.GetById(vaultKeepsId);
      if (foundVault == null)
      {
        throw new Exception("Invalid Id");
      }
      if (foundVault.Vault.IsPrivate == true && foundVault.Vault.CreatorId != id)
      {
        throw new Exception("This aint it dude");
      }
      return foundVault;
    }

    public void RemoveVaultKeep(int vaultKeepId, string userId)
    {
      VaultKeep foundVaultKeep = GetById(vaultKeepId);
      if(foundVaultKeep.CreatorId != userId)
      {
        throw new Exception("This is not your vault keep");
      }
      _vaultKeepsRepository.Delete(vaultKeepId);
    }
}
}