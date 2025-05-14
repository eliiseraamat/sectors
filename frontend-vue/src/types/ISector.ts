export interface ISector {
  id: number
  name: string
  parentSectorId: string
  childSectors: ISector[]
}
