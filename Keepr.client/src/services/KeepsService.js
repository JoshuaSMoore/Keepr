import { AppState } from "../AppState"
import { Keep } from "../models/Keep.js"
import { VaultKeep } from "../Models/VaultKeep.js"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService{
  async getAll(){
    AppState.keeps = []
    const res = await api.get('api/keeps')
    logger.log('all keeps res', res)
    AppState.keeps = res.data.map(keep => new Keep(keep))
  }
  async getById(keepId){
    const res = await api.get(`api/keeps/${keepId}`)
  }
  async createKeep(keepData){
    const res = await api.post('api/keeps', keepData)
    logger.log('createKeep res', res)
    AppState.keeps.unshift(new Keep(res.data))
  }
  async deleteKeep(keepId){
    const res = await api.delete(`api/keeps/${keepId}`)
    logger.log('DeleteKeep res', res)
    AppState.keeps = AppState.keeps.filter(keep => keep.id !== keepId)
  }
  async editKeep(keepData){
    const res = await api.put(`api/keeps/${keepData.id}`, keepData)
    logger.log('editKeep res', res)
    let keep = AppState.keeps.findIndex(keep => keep.id === keepData.id)
    AppState.keeps.splice(keep, 1, new Keep(res.data))
}
async addVaultKeep(vaultKeepData){
  logger.log('addVaultKeep res', vaultKeepData)
  const res = await api.post(`api/vaultkeeps`, vaultKeepData)
  AppState.vaultkeeps.unshift(new VaultKeep(res.data))
}
}
export const keepsService = new KeepsService()