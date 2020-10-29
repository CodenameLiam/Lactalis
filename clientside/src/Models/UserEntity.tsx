export interface IUserEntity {
	email: string;
	password: string;
	_confirmPassword: string;
}

export const UserFields: ReadonlyArray<keyof IUserEntity> = [
	"email",
	"password",
	"_confirmPassword",
];
