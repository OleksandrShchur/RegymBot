import { RegymClub } from "./regym-club";

export class UserModel {
  userGuid: string;
  name: string;
  surName: string;
  description: string;
  category: Number;
  imageUrl: string;
  role: Array<string>;
  clubs: Array<RegymClub>;
}
