export interface ChangePasswordInput{
  currentPassword:string;
  newPassword:string;
}

export interface ProfileDto{
  userName?:string;
  email?:string;
  name?:string;
  surname?:string;
  phoneNumber?:string;
}

