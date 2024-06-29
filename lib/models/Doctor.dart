import 'dart:convert';

import 'AppointmentDTO.dart';
import 'Available.dart';

class Doctor {
  String? username;
  String? specialization;
  String? name;
  String? phoneNumber;
  String? email;
  List<Appointment>? appointment;
  List<String>? addresses;
  List<Available>? available;
  Doctor({
    this.username,
    this.specialization,
    this.name,
    this.phoneNumber,
    this.email,
    this.appointment,
    this.addresses,
    this.available,
  });

  Map<String, dynamic> toMap() {
    return {
      'username': username,
      'specialization': specialization,
      'name': name,
      'phoneNumber': phoneNumber,
      'email': email,
      'appointment': appointment?.map((x) => x?.toMap())?.toList(),
      'addresses': addresses,
      'available': available?.map((x) => x?.toMap())?.toList(),
    };
  }

  factory Doctor.fromMap(Map<String, dynamic> map) {
    return Doctor(
      username: map['username'],
      specialization: map['specialization'],
      name: map['name'],
      phoneNumber: map['phoneNumber'],
      email: map['email'],
      appointment: map['appointment'] != null ? List<Appointment>.from(map['appointment']?.map((x) => Appointment.fromMap(x))) : null,
      addresses: List<String>.from(map['addresses']),
      available: map['available'] != null ? List<Available>.from(map['available']?.map((x) => Available.fromMap(x))) : null,
    );
  }

  String toJson() => json.encode(toMap());

  factory Doctor.fromJson(String source) => Doctor.fromMap(json.decode(source));
}
