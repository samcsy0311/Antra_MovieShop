export interface User {
  family_name: string;
  given_name: string;
  email: string;
  role: Array<string>;
  exp: string;
  alias: string;
  isAdmin: string;
  birthdate: Date;
  nameid: number;
}
