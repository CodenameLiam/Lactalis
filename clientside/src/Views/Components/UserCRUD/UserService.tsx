import axios from "axios";
import User from "Models/Entities/User";
import { SERVER_URL } from "Constants";

export const getUsers = async (): Promise<User[]> => {
	const response = await axios.get(`${SERVER_URL}/api/account`);
	const usersJson = response.data as Array<{}>;
	return usersJson.map((userData) => new User(userData));
};

export const getUser = async (id: string): Promise<User> => {
	const response = await axios.get(`${SERVER_URL}/api/account/${id}`);
	return new User({
		...response.data,
		groups: response.data.groups.map((g: any) => g.name),
	});
};

export const createUser = async (userJson: {}) => {
	const response = await axios.post(`${SERVER_URL}/api/account`, userJson);
	return response.data;
};

export const updateUser = async (userJson: {}) => {
	const response = await axios.put(`${SERVER_URL}/api/account`, userJson);
	return response.data;
};

export const deleteUser = async (id: string) => {
	return await axios.delete(`${SERVER_URL}/api/account?id=${id}`);
};
