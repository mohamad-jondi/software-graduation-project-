import 'dart:convert';

class User {
  String? username;
  String? name;
  String? email;
  String? password;
  String? phoneNumber;
  String? userType;
  String? personType;
  bool? isAdmin;
  bool? isVerifiedDoctor;
  User(
      {this.username,
      this.name,
      this.email,
      this.password,
      this.phoneNumber,
      this.userType,
      this.personType,
      this.isAdmin,
      this.isVerifiedDoctor});

  Map<String, dynamic> toMap() {
    return {
      'username': username,
      'name': name,
      'email': email,
      'password': password,
      'phoneNumber': phoneNumber,
      'userType': userType,
      'personType': personType,
    };
  }

  factory User.fromMap(Map<String, dynamic> map) {
    return User(
      username: map['username'],
      name: map['name'],
      email: map['email'],
      password: map['password'],
      phoneNumber: map['phoneNumber'],
      userType: map['userType'],
      personType: map['personType'],
      isAdmin: map['isAdmin'],
      isVerifiedDoctor: map['isVerifiedDoctor'],
    );
  }

  String toJson() => json.encode(toMap());

  factory User.fromJson(String source) => User.fromMap(json.decode(source));
}
