using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vaultKeepsRepository;
    private readonly VaultsRepository _vaultsRepository;

    private readonly KeepsRepository _keepsRepository;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsRepository vaultsRepository, KeepsRepository keepsRepository)
    {
      _vaultKeepsRepository = vaultKeepsRepository;
      _vaultsRepository = vaultsRepository;
      _keepsRepository = keepsRepository;
    }
    public VaultKeep Post(VaultKeep vaultKeepData)
    {
      _keepsRepository.KeepsCounter(vaultKeepData.KeepId, 1);
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