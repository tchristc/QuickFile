import * as axios from 'axios';

const BASE_URL = 'https://localhost:44389';

function upload(formData) {
    const url = `${BASE_URL}/api/files`;
    return axios.post(url, formData)
        // get data
        .then(x => x.data)
        // add url field
        .then(x => x.map(f => Object.assign({},
            f.FileData, { url: `${BASE_URL}/api/files/${f.Id}` })));
}

export { upload }