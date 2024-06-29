import 'dart:convert';

import 'Drug.dart';

class Case {
  String? patientUsername;
  String? doctorUserName;
  String? nurseUserName;
  String? caseDescription;
  String? title;
  DateTime? createdDate;
  String? notes;
  DateTime? lastUpdated;
  List<Drug>? drugs;
  Case({
    this.patientUsername,
    this.doctorUserName,
    this.nurseUserName,
    this.caseDescription,
    this.title,
    this.createdDate,
    this.notes,
    this.lastUpdated,
    this.drugs,
  });


  Map<String, dynamic> toMap() {
    return {
      'patientUsername': patientUsername,
      'doctorUserName': doctorUserName,
      'nurseUserName': nurseUserName,
      'caseDescription': caseDescription,
      'title': title,
      'createdDate': createdDate?.toString(),
      'notes': notes,
      'lastUpdated': lastUpdated?.toString(),
      'drugs': drugs?.map((x) => x?.toMap())?.toList(),
    };
  }

  factory Case.fromMap(Map<String, dynamic> map) {
    return Case(
      patientUsername: map['patientUsername'],
      doctorUserName: map['doctorUserName'],
      nurseUserName: map['nurseUserName'],
      caseDescription: map['caseDescription'],
      title: map['title'],
      createdDate: map['createdDate'] != null ? DateTime.parse(map['createdDate']) : null,
      notes: map['notes'],
      lastUpdated: map['lastUpdated'] != null ? DateTime.parse(map['lastUpdated']) : null,
      drugs: map['drugs'] != null ? List<Drug>.from(map['drugs']?.map((x) => Drug.fromMap(x))) : null,
    );
  }

  String toJson() => json.encode(toMap());

  factory Case.fromJson(String source) => Case.fromMap(json.decode(source));
}
