import type { IForm } from '@/types/IForm'
import { BaseService } from './BaseService'
import type { ISector } from '@/types/ISector'
import type { IResult } from '@/types/IResult'
import type { IFormWithSectors } from '@/types/IFormWithSectors'

export class SectorService extends BaseService {
  private basePath: string = 'forms'

  async getAllAsync(): Promise<IResult<ISector[]>> {
    try {
      const response = await BaseService.axios.get<ISector[]>(this.basePath + '/sectors')

      if (response.status <= 300) {
        return { data: response.data }
      }
      console.log('getSectors error', response)
      return {
        errors: [response.status.toString + ' ' + response.statusText.trim()],
      }
    } catch (error) {
      console.log('error: ', (error as Error).message)
      return {
        errors: [JSON.stringify(error)],
      }
    }
  }

  async saveFormAsync(form: IForm): Promise<IResult<IForm>> {
    try {
      const response = await BaseService.axios.post<IForm>(this.basePath, form)

      console.log('saveForm response', response)
      if (response.status <= 300) {
        return { data: response.data }
      }
      console.log('saveForm error', response)
      return {
        errors: [response.status.toString + ' ' + response.statusText.trim()],
      }
    } catch (error) {
      console.log('error: ', (error as Error).message)
      return {
        errors: [JSON.stringify(error)],
      }
    }
  }

  async saveSectorInFormsAsync(updateFormSelectors: IFormWithSectors): Promise<IResult<void>> {
    try {
      const response = await BaseService.axios.post<void>(
        this.basePath + '/sector-in-form',
        updateFormSelectors,
      )

      if (response.status <= 300) {
        return { data: response.data }
      }
      console.log('saveForm error', response)
      return {
        errors: [response.status.toString + ' ' + response.statusText.trim()],
      }
    } catch (error) {
      console.log('error: ', (error as Error).message)

      return {
        errors: [JSON.stringify(error)],
      }
    }
  }

  async updateFormWithSectorsAsync(updateFormSelectors: IFormWithSectors): Promise<IResult<void>> {
    try {
      const response = await BaseService.axios.put<void>(this.basePath, updateFormSelectors)

      console.log('saveForm response', response)
      if (response.status <= 300) {
        return { data: response.data }
      }
      console.log('saveForm error', response)

      return {
        errors: [response.status.toString + ' ' + response.statusText.trim()],
      }
    } catch (error) {
      console.log('error: ', (error as Error).message)
      return {
        errors: [JSON.stringify(error)],
      }
    }
  }

  async getFormWithSectorsAsync(id: number): Promise<IResult<IFormWithSectors>> {
    try {
      const response = await BaseService.axios.get<IFormWithSectors>(
        this.basePath + `/form-with-sectors/${id}`,
      )

      console.log('getAll response', response)
      if (response.status <= 300) {
        return { data: response.data }
      }
      console.log('getAll error', response)
      return {
        errors: [response.status.toString + ' ' + response.statusText.trim()],
      }
    } catch (error) {
      console.log('error: ', (error as Error).message)
      return {
        errors: [JSON.stringify(error)],
      }
    }
  }
}
