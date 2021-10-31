import { AppState } from "../AppState"
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
  let vault = AppState.vaults.find(vault => vault.id === vaultData.id)
  AppState.vault = new Vault(res.data)
}
}
export const vaultsService = new VaultsService()