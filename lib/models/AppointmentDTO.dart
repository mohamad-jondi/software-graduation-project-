import 'dart:convert';

import 'package:intl/intl.dart';

class Appointment {
  int? appointmentId;
  DateTime? date;
  String? doctorName;
  String? patientName;
  String? status;
  String? description;
  String? notes;
  Appointment({
    this.appointmentId,
    this.date,
    this.doctorName,
    this.patientName,
    this.status,
    this.description,
    this.notes,
  });

  Map<String, dynamic> toMap() {
    return {
      'appointmentId': appointmentId,
      'date': date.toString(),
      'doctorName': doctorName,
      'patientName': patientName,
      'status': status,
      'description': description,
      'notes': notes,
    };
  }

  Map<String, String> toAMap() {
    return {
      'appointmentId': appointmentId.toString(),
      'date': date.toString(),
      'doctorName': doctorName.toString(),
      'patient': patientName.toString(),
      'status': status == 'Accepted' ? 'Confirmed' : status.toString(),
      'description': description.toString(),
      'notes': notes.toString(),
      "time": DateFormat('hh:mm a').format(date!)
    };
  }

  factory Appointment.fromMap(Map<String, dynamic> map) {
    DateTime d = DateTime.now();
    if (map['date'] != null) {
      var a = DateTime.parse(map['date']);
      d = DateTime(a.year, a.month, a.day, a.hour, a.second);
    }
    return Appointment(
      appointmentId: map['appointmentId'],
      date: d,
      doctorName: map['doctorName'],
      patientName: map['patientName'],
      status: map['status'],
      description: map['description'],
      notes: map['notes'],
    );
  }

  String toJson() => json.encode(toMap());

  factory Appointment.fromJson(String source) =>
      Appointment.fromMap(json.decode(source));
}
