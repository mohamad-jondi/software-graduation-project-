import 'dart:convert';

import 'package:flutter_app/models/AppointmentDTO.dart';
import 'package:flutter_app/models/Vacc.dart';

class Child {
  int? id;
  String? name;
  DateTime? dateOfBirth;
  double? latestRecordedWeight;
  double? latestRecordedHeight;
  int? gender;
  List? cases;
  List<Appointment>? appointments;
  List<Vaccination>? vaccination;
  Child({
    this.id,
    this.name,
    this.dateOfBirth,
    this.latestRecordedWeight,
    this.latestRecordedHeight,
    this.gender,
    this.cases,
    this.appointments,
    this.vaccination,
  });

  Map<String, dynamic> toMap() {
    return {
      'id': id,
      'name': name,
      'dateOfBirth': dateOfBirth.toString(),
      'latestRecordedWeight': latestRecordedWeight,
      'latestRecordedHeight': latestRecordedHeight,
      'gender': gender,
      'appointments': appointments?.map((x) => x?.toMap())?.toList(),
    };
  }

  factory Child.fromMap(Map<String, dynamic> map) {
    return Child(
      id: map['id']?.toInt(),
      name: map['name'],
      dateOfBirth: map['dateOfBirth'] != null ? DateTime.parse(map['dateOfBirth']) : DateTime.now(),
      latestRecordedWeight: map['latestRecordedWeight']?.toDouble(),
      latestRecordedHeight: map['latestRecordedHeight']?.toDouble(),
      gender: map['gender']?.toInt(),
      // cases: map['cases'] != null ? List.fromMap(map['cases']) : null,
      appointments: map['appointments'] != null ? List<Appointment>.from(map['appointments']?.map((x) => Appointment.fromMap(x))) : [],
      vaccination: map['vaccination'] != null ? List<Vaccination>.from(map['vaccination']?.map((x)=>Vaccination.fromMap(x))) : [],

    );
  }

  String toJson() => json.encode(toMap());

  factory Child.fromJson(String source) => Child.fromMap(json.decode(source));
}
