export class CreateUserModel {
    constructor(
    public username: string,
    public password: string,
    public confirmPassword: string,
    public email: string,
    public address: string,
    public phoneNumber: string,
    public firstName: string,
    public lastName: string,
    public birthDate: string,
    public role: string) {}
}

export class EditProfileModel {
    constructor(
        public username: string,
        public email: string,
        public firstName: string,
        public lastName: string,
        public birthDate: string,
        public address: string,
        public phoneNumber: string) {}
}