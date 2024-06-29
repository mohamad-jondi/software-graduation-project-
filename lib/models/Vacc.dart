import 'dart:convert';

class Vaccination {
  int? vaccinationID;
  String? name;
  DateTime? administeredDate;
  String? description;
  String? vaccineStatus;
  int? shotsLeft;
  Vaccination({
    this.vaccinationID,
    this.name,
    this.administeredDate,
    this.description,
    this.vaccineStatus,
    this.shotsLeft,
  });

  Map<String, dynamic> toMap() {
    return {
      'vaccinationID': vaccinationID,
      'name': name,
      'administeredDate': administeredDate?.millisecondsSinceEpoch,
      'description': description,
      'vaccineStatus': vaccineStatus,
      'shotsLeft': shotsLeft,
    };
  }

  factory Vaccination.fromMap(Map<String, dynamic> map) {
    return Vaccination(
      vaccinationID: map['vaccinationID']?.toInt(),
      name: map['name'],
      administeredDate: map['administeredDate'] != null
          ? DateTime.parse(map['administeredDate'])
          : null,
      description: map['description'],
      vaccineStatus: map['vaccineStatus'],
      shotsLeft: map['shotsLeft']?.toInt(),
    );
  }

  String toJson() => json.encode(toMap());

  factory Vaccination.fromJson(String source) =>
      Vaccination.fromMap(json.decode(source));
}
