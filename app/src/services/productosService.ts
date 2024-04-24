// userService.ts

import axios, { AxiosResponse } from 'axios';
import {Producto} from '../models/productosModel';

const BASE_URL = 'http://localhost:9094/api/Products';
const API_KEY = 'dGIzcWJ4YWU0dTFxYXk5a3o5NnFiMmQ5N2xtcTVwZzA='; // Tu clave API aquí

axios.interceptors.request.use(config => {
    config.headers['x-api-key'] = API_KEY;
    return config;
  });


export default {
  getProductos(): Promise<AxiosResponse<Producto[]>> {
    return axios.get<Producto[]>(BASE_URL);
  },
  getProductosId(id: number): Promise<AxiosResponse<Producto>> {
    return axios.get<Producto>(`${BASE_URL}?id=${id}`);
  },
  createProducto(producto: Producto): Promise<AxiosResponse<Producto[]>> {
    return axios.post<Producto[]>(BASE_URL, producto);
  },
  updateProducto(producto: Producto): Promise<AxiosResponse<Producto[]>> {
    return axios.patch<Producto[]>(BASE_URL, producto);
  },
  deleteProducto(id: number): Promise<AxiosResponse<Producto[]>> {
    return axios.delete<Producto[]>(`${BASE_URL}?id=${id}`);
  },
};
