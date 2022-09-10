import { RegymClub } from "./regym-club";

export interface EnrollModel {
    clientGuid: string;
    name: string;
    enrol: string;
    selectedClub: RegymClub;
    proceed: boolean;
    phone: string;
    dateCreated: Date;
}