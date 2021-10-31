import { AppState } from "../AppState"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService{
  async getAll(){
    const res = await api.get('api/keeps')
    logger.log('all keeps res', res)
    AppState.keeps = res.data.map(keep => new Keep(keep))
  }
  async getById(keepId){
    const res = await api.get(`api/keeps/${keepId}`)
    logger.log('getById res', res)
    AppState.keep = new Keep(res.data)
  }
  async createKeep(keepData){
    const res = await api.post('api/keeps', keepData)
    logger.log('createKeep res', res)
    AppState.keep = [new Keep(res.data), ...AppState.keeps]
  }
  async deleteKeep(keepId){
    const res = await api.delete(`api/keeps/${keepId}`)
    logger.log('DeleteKeep res', res)
    AppState.keeps = AppState.keeps.filter(keep => keep.id !== keepId)
  }
  async editKeep(keepData){
    const res = await api.put(`api/keeps/${keepData.id}`, keepData)
    logger.log('editKeep res', res)
    let keep = AppState.keeps.find(keep => keep.id === keepData.id)
    AppState.keep = new Keep(res.data)
}
}
export const keepsService = new KeepsService()