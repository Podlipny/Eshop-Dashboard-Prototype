import { IContact } from './IContact';

export interface IVendor {
  id: string;
  name: string;
  director: string;
  ico: number;
  dic: number;
  contact: IContact;
}
