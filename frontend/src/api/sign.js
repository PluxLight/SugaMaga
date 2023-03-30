import { apiInstance } from "./index.js";

const api = apiInstance();

function signup(userInfo, config, success, fail) {
    api.post(`sign/up`, JSON.stringify(userInfo), config).then(success).catch(fail);
}

function searchNickname(param, success, fail) {
  api.get(`user/nickname`, { params: param }).then(success).catch(fail);
}

function getUserInfo(config, success, fail) {
  api.get(`user`, config).then(success).catch(fail);
}

function putUserNickname(nickname, config, success, fail) {
  api.put(`user`, JSON.stringify(nickname), config).then(success).catch(fail);
}




export { signup, searchNickname, getUserInfo, putUserNickname };
