import { AppState } from "../AppState"
import { Keep } from "../Models/Keep.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class VaultsService {
async getVault(vaultId){
  const res = await api.get(`api/vaults/${vaultId}`)
  logger.log('vault res', res)
  AppState.vaults = new Vault(res.data)
}
async deleteVault(vaultId){
  const res = await api.delete(`api/vaults/${vaultId}`)
  logger.log('delete vault res', res)
  AppState.vaults = AppState.vaults.filter(vault => vault.id !== vaultId)
}
async editVault(vaultData)
{
  const res = await api.put(`api/vaults/${vaultData.id}`, vaultData)
  logger.log('edit vault res', res)
  let vault = AppState.vaults.findIndex(vault => vault.id === vaultData.id)
  AppState.vaults.splice(vault, 1, new Vault(res.data))
}
async createVault(vaultData){
  const res = await api.post(`api/vaults`, vaultData)
  logger.log('create vault res', res)
  AppState.vaults.unshift(new Vault(res.data))
}
async getKeepByVault(vaultId){
  AppState.keeps = []
  const res = await api.get(`api/vaults/${vaultId}/keeps`)
  logger.log('keeps res', res)
  AppState.keeps = res.data.map(keep => new Keep(keep))
}
}
export const vaultsService = new VaultsService()