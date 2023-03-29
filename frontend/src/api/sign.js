import { apiInstance } from "./index.js";

const api = apiInstance();

function signup(userInfo, config, success, fail) {
    api.post(`sign/up`, JSON.stringify(userInfo), config).then(success).catch(fail);
}

function searchNickname(param, success, fail) {
  api.get(`user/nickname`, { params: param }).then(success).catch(fail);
}

export { signup, searchNickname };
