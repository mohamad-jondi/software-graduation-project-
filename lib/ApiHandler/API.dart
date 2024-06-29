import "dart:convert";
import "dart:developer";
import "dart:io";

import "package:http/http.dart" as http;

class API {
  API._();
  static API apis = API._();
  String server = "https://32d1-188-161-50-223.ngrok-free.app";
  String auth = "";

  getUnverifiedDoctors() async {
    var endpoint = "api/admin/unverified-doctors";
    return await http.get(Uri.parse("$server/$endpoint"));
  }

  verifyDoctors(String username) async {
    var endpoint = "api/admin/verify-doctors/$username";
    return await http.post(Uri.parse("$server/$endpoint"));
  }

  rejectDoctors(String username) async {
    var endpoint = "api/admin/verify-doctors/$username?reject=true";
    return await http.post(Uri.parse("$server/$endpoint"));
  }

  getDoctorCredientials(String username) async {
    var endpoint = "api/doctor/credential/$username";
    return await http.get(Uri.parse("$server/$endpoint"));
  }

  deleteDoctorCredientials(String id) async {
    var endpoint = "api/doctor/credential/$id";
    return await http.delete(Uri.parse("$server/$endpoint"));
  }

  uploadDoctorCrediential(
      String username, String fileName, String base64File) async {
    var endpoint = "api/doctor/credential/$username";
    return await http.post(Uri.parse("$server/$endpoint"),
        body: {"fileName": fileName, "base64Image": base64File});
  }

  doctorSignup(Map<String, dynamic> map) async {
    // var request = http.MultipartRequest(
    //       'PUT', Uri.parse(server+""));

    //   map.forEach((key, value) {
    //     request.fields[key] = value.toString();
    //   });
    //   request.headers.addAll(
    //       {'content-type': 'multipart/form-data'});
    //   // request.files.add(await http.MultipartFile.fromPath(
    //   //   "file",
    //   //   file.path,
    //   // ));
    //   final response = await request.send();
    try {
      final res = await http.post(Uri.parse("$server/user/register"),
          headers: {'Content-Type': 'application/json; charset=UTF-8'},
          body: jsonEncode(map));
      log(res.body);
      log(res.statusCode.toString());
      return res;
    } catch (e) {
      return null;
    }
  }

  getDoctors() async {
    return await http.get(Uri.parse("$server/api/Patient/BrowseDoctors"));
  }

  setUserType(String username, int type) async {
    final res = await http.post(
        Uri.parse("$server/Person/EditPersonType/$username"),
        headers: {'Content-Type': 'application/json; charset=UTF-8'},
        body: jsonEncode({"personType": type}));
  }

  uploadImage(File file, String username) async {
    final bytes = file.readAsBytesSync();
    final base64Image = base64Encode(bytes);
    final fileName = '$username.png';
    final Map<String, dynamic> payload = {
      'fileName': fileName,
      'base64Image': base64Image,
    };
    final res = await http.post(Uri.parse("$server/api/Picture/upload"),
        headers: {'Content-Type': 'application/json; charset=UTF-8'},
        body: jsonEncode(payload));
    log(res.statusCode.toString());
    return res;
  }

  signin(Map<String, dynamic> map) async {
    try {
      final res = await http.post(Uri.parse("$server/user/login"),
          headers: {'Content-Type': 'application/json; charset=UTF-8'},
          body: jsonEncode(map));
      log(res.body);
      return res;
    } catch (e) {
      return null;
    }
  }

  updateUser(Map<String, dynamic> map) async {
    try {
      final res = await http.put(Uri.parse('$server/Person/updateinfo'),
          headers: {'Content-Type': 'application/json; charset=UTF-8'},
          body: jsonEncode(map));
      log(res.body);
      log(res.statusCode.toString());
      return res;
    } on Exception {
      // TODO
      return null;
    }
  }

  getUserChats(String username) async {
    try {
      final res = await http
          .get(Uri.parse('$server/api/Chat/GetChatsByUser/$username'));
      return res;
    } on Exception {
      // TODO
      return null;
    }
  }

  getChatMessages(String username, int chatId) async {
    return await http.get(
        Uri.parse('$server/api/Chat/GetChatsByUser/$username/Browse/$chatId'));
  }

  setChatRead(int messageId) async {
    final res = await http.post(
      Uri.parse('$server/api/Chat/chat/messages/$messageId/setChatAsRead'),
      headers: {'Content-Type': 'application/json; charset=UTF-8'},
    );
    log(res.statusCode.toString());
    log(res.body);
    return res;
  }

  getPatientAps(String username) async {
    return await http.get(Uri.parse(
        '$server/api/Patient/ViewUpcomingAppointments?patientUsername=${username}'));
  }

  reqAppoint(Map<String, dynamic> map) async {
    log(map['appointmentDate']);
    return await http.post(
        Uri.parse(
            '$server/api/Patient/RequestAppointment?patientUsername=${map['patientUsername']}&doctorUsername=${map['doctorUsername']}&appointmentDate=${map['appointmentDate']}&Description=${map['Description']}'),
        headers: {'Content-Type': 'application/json; charset=UTF-8'},
        body: jsonEncode(map));
  }

  sendMessage(String sender, String receiver, Map<String, dynamic> map) async {
    try {
      final res = await http.post(
          Uri.parse(
              '$server/api/Chat/SendMessage?senderUsername=$sender&receiverUsername=$receiver'),
          headers: {'Content-Type': 'application/json; charset=UTF-8'},
          body: jsonEncode(map));
      log(res.body);
      log(res.statusCode.toString());
      return res;
    } on Exception {
      // TODO
      return null;
    }
  }

  changeAppointmentStatus(Map<String, dynamic> map) async {
    try {
      log(map.toString());
      final res = await http.put(
          Uri.parse('$server/api/Doctor/manage-appointment'),
          headers: {'Content-Type': 'application/json; charset=UTF-8'},
          body: jsonEncode(map));
      return res;
    } on Exception {
      // TODO
      return null;
    }
  }

  getAvail(String doctorId) async {
    return await http
        .get(Uri.parse('$server/api/Doctor/availability/$doctorId'));
  }

  getAAppointment(String username) async {
    try {
      final res = await http
          .get(Uri.parse('$server/api/Doctor/accepted-appointments/$username'));
      return res;
    } on Exception {
      // TODO
      return null;
    }
  }

  getPAppointment(String username) async {
    try {
      final res = await http
          .get(Uri.parse('$server/api/Doctor/pending-appointments/$username'));
      return res;
    } on Exception {
      // TODO
      return null;
    }
  }
}
