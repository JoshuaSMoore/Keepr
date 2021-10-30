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

    public VaultKeep GetById(int vaultKeepId, string id)
    {
    VaultKeep foundVaultKeep =  _vaultKeepsRepository.GetById(vaultKeepId);
    if(foundVaultKeep == null)
    {
      throw new Exception("Invalid Id");
    }
    if (foundVaultKeep.Vault.IsPrivate == true && foundVaultKeep.CreatorId != id)
    {
      throw new Exception("You do not have access to this VaultKeep");
      
    }
    return foundVaultKeep;
    }

    public void RemoveVaultKeep(int vaultKeepId, string userId)
    {
      VaultKeep foundVaultKeep = _vaultKeepsRepository.GetById(vaultKeepId);
      if(foundVaultKeep.CreatorId != userId)
      {
        throw new Exception("This is not your vault keep");
      }
      _vaultKeepsRepository.Delete(vaultKeepId);
    }
    
  }
}