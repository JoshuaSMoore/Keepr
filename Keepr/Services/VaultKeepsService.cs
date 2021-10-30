using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vaultKeepsRepository;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository)
    {
      _vaultKeepsRepository = vaultKeepsRepository;
    }
    public VaultKeep Post(VaultKeep vaultKeepData)
    {
      return _vaultKeepsRepository.Create(vaultKeepData);
    }

    public VaultKeep GetById(int vaultKeepId)
    {
      VaultKeep foundVaultKeep = _vaultKeepsRepository.GetById(vaultKeepId);
      if(foundVaultKeep == null)
      {
        throw new Exception("Invalid Id");
      }
      return foundVaultKeep;
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