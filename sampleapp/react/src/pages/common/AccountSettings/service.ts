import request from "@/utils/request";
import { ChangePasswordInput, ProfileDto } from "./data";


export async function changePassword(input: ChangePasswordInput) {
  return request('api/identity/my-profile/change-password', {
    method: 'POST',
    data:input,
  });
}
export async function getMyProfile() {
  return request('api/identity/my-profile', {
    method: 'GET',
  });
}

export async function updateMyProfile(input:ProfileDto) {
  return request('api/identity/my-profile', {
    method: 'PUT',
    data:input,
  });
}

