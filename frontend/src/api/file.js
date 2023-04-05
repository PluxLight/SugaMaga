import { apiInstance } from "./index.js";

const api = apiInstance();


function getImage(fileName, success, fail) {
  api.get(`file/images/${fileName}`).then(success).catch(fail);
}

function getImageList(param, success, fail) {
    api.get(`file/images`, { params: param }).then(success).catch(fail);
  }

export { getImage, getImageList};
