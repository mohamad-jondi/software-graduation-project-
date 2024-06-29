import 'dart:async';
import 'dart:convert';
import 'dart:developer';
import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter_app/ApiHandler/API.dart';
import 'package:flutter_app/models/Allergy.dart';
import 'package:flutter_app/models/AppointmentDTO.dart';
import 'package:flutter_app/models/Available.dart';
import 'package:flutter_app/models/Case.dart';
import 'package:flutter_app/models/ChatModel.dart';
import 'package:flutter_app/models/Child.dart';
import 'package:flutter_app/models/Doctor.dart';
import 'package:flutter_app/models/Message.dart';
import 'package:flutter_app/models/Not.dart';
import 'package:flutter_app/models/UserModel.dart';
import 'package:http/http.dart' as http;
import 'package:image_picker/image_picker.dart';
import 'package:intl/intl.dart';

class AppProvider extends ChangeNotifier {
  AppProvider() {
    Timer.periodic(Duration(seconds: 5), (timer) {
      if (isLogged) {
        getNots();
      }
      print('Running a scheduled task at ${DateTime.now()}');
    });
  }
  bool isLogged = false;
  late User loggedUser;
  List<Doctor> doctors = [];
  List<Doctor> filteredDoctors = [];
  String searchText = "";
  String selectedCategory = "General";
  TextEditingController usernameController = TextEditingController();
  TextEditingController emailController = TextEditingController();
  TextEditingController phoneController = TextEditingController();
  TextEditingController passwordController = TextEditingController();
  TextEditingController nameController = TextEditingController();
  TextEditingController messageController = TextEditingController();
  TextEditingController notesController = TextEditingController();
  TextEditingController vnameController = TextEditingController();
  TextEditingController descriptionController = TextEditingController();
  TextEditingController administeredDateController = TextEditingController();
  TextEditingController shotsLeftController = TextEditingController();
  List<Appointment> aappointments = [];
  List<Appointment> pappointments = [];
  List<Chat> chats = [];
  List<Available> avs = [];
  List<Appointment> aps = [];
  List<Appointment> patientAps = [];
  List<Child> children = [];
  List<Not> nots = [];
  int chosenDay = 1;
  int start = 0;
  int end = 0;
  Child? selectedChild;
  Case? selectedCase;
  List<Allergy> allergies = [];
  Appointment? selectedAppointment;
  Map<String, dynamic> daysMap = {
    "Sunday": 0,
    "Monday": 1,
    "Tuesday": 2,
    "Wednesday": 3,
    "Thursday": 4,
    "Friday": 5,
    "Saturday": 6,
  };

  String? chosenTime;
  getAllergy(String username) async {
    log(username);
    final res = await API.apis.getHistory(username);
    log(res.body);
    log(res.statusCode.toString());
    if (res.statusCode == 200) {
      final data = jsonDecode(res.body);
      log(((data as Map)['allergies'] as List).toString());
      allergies = ((data as Map)['allergies'] as List)
          .map((e) => Allergy.fromMap(e))
          .toList();
    }
  }

  getCase(int id) async {
    final res = await API.apis.getCase(id);
    if (res.statusCode == 200) {
      selectedCase = Case.fromJson(res.body);
      notifyListeners();
    }
  }

  deleteVac(int id) async {
    final res = await API.apis.deleteVac(id);
    if (res.statusCode == 200) {
      selectedChild!.vaccination !=
          selectedChild!.vaccination!
              .where((e) => e.vaccinationID != id)
              .toList();
      notifyListeners();
    }
  }

  snooze() async {
    final res = await API.apis.snooze(loggedUser.username!, 15);
    if (res.statusCode == 200) {
      await getAAPointments();
      await getPAPointments();
      notifyListeners();
    }
    log(res.body);
  }

  addVacc(Map<String, dynamic> map) async {
    final res = await API.apis.addVac(selectedChild!.id!, map);
    if (res.statusCode == 200) {
      await getChildren();
      selectedChild = children.firstWhere((e) => e.id == selectedChild!.id);
      notifyListeners();
    }
  }

  getChildren() async {
    final res = await API.apis.getChildren(loggedUser.username!);
    if (res.statusCode == 200) {
      final data = jsonDecode(res.body);
      children = (data as List).map((e) => Child.fromMap(e)).toList();
      notifyListeners();
    }
  }

  getNots() async {
    final res = await API.apis.getNots(loggedUser.username!);
    if (res.statusCode == 200) {
      final data = jsonDecode(res.body);
      nots = (data as List).map((e) => Not.fromMap(e)).toList();
      log(nots.first.toMap().toString());
      notifyListeners();
    }
  }

  isUpcoming(Appointment a) {
    return (a.status == 'Accepted' || a.status == 'Pending') &&
        DateTime.now().compareTo(a.date!) < 0;
  }

  isPast(Appointment a) {
    return a.status == 'Accepted' && DateTime.now().compareTo(a.date!) > 0;
  }

  isCancled(Appointment a) {
    return a.status == 'Canceled';
  }

  getPatienAps() async {
    final res = await API.apis.getPatientAps(loggedUser.username!);
    log(res.body);
    if (res.statusCode == 200) {
      patientAps = (jsonDecode(res.body) as List)
          .map((e) => Appointment.fromMap(e))
          .toList();
      log(patientAps.length.toString());
      log(patientAps.first.toJson());
    }
  }

  reqApp() async {
    final res = await API.apis.reqAppoint(<String, dynamic>{
      "appointmentDate": DateFormat("yyyy-MM-dd'T'HH:mm:ss.SSS'Z'")
          .format(DateTime(
              DateTime.now().year,
              DateTime.now().month,
              DateTime.now().day + chosenDay,
              int.parse(chosenTime?.split(":")[0] ?? '0')))
          .toString(),
      "doctorUsername": selectedDoctor.username,
      "patientUsername": loggedUser.username,
      "Description": notesController.text
    });
    log(res.body);
    if (res.statusCode == 200) {
      notesController.clear();
      notifyListeners();
    }
  }

  figureChat(String username) async {
    for (int i = 0; i < chats.length; i++) {
      if (chats[i].firstPartyUserName == selectedDoctor.username ||
          chats[i].secondPartyUsername == selectedDoctor.username) {
        await getChatMessages(chats[i].chatId!);
        notifyListeners();
        break;
      }
    }
  }

  setChosenDay(int i) {
    chosenDay = i;
    log(DateFormat('EEEE').format(DateTime.now().add(Duration(days: i))));
    int day = daysMap[
        DateFormat('EEEE').format(DateTime.now().add(Duration(days: i)))];
    start = 0;
    end = 0;

    for (int i = 0; i < avs.length; i++) {
      if (avs[i].dayOfWeek == day) {
        log(i.toString());
        start = int.parse(avs[i].startHour!.split(":")[0]);
        end = int.parse(avs[i].endHour!.split(":")[0]);
        break;
      }
    }
    log(start.toString() + "  " + end.toString());
    notifyListeners();
  }

  choosteTime(String x) {
    chosenTime = x;
    notifyListeners();
  }

  setChatRead(int chatId) async {
    for (int i = 0; i < chats.length; i++) {
      if (chats[i].chatId == chatId) {
        if (chats[i].numberOfMessages != 0 &&
            chats[i].numberOfMessages != null) {
          await API.apis.setChatRead(chats[i].messages!.first.chatMessageID!);
        }
        chats[i].numberOfMessages = 0;
        log("message");
        break;
      }
      notifyListeners();
    }
  }

  sendMessage(String receiver) async {
    final res = await API.apis.sendMessage(
        loggedUser.username!, receiver, <String, dynamic>{
      "messageContent": messageController.text,
      "isRead": false,
      "isDeleted": false
    });
    if (res.statusCode == 200) {
      final data = jsonDecode(res.body);
      var chatss = chats
          .where((e) =>
              e.firstPartyUserName == receiver ||
              e.secondPartyUsername == receiver)
          .toList();
      if (chatss.isEmpty) {
        await getChats();
        chatss = chats
            .where((e) =>
                e.firstPartyUserName == receiver ||
                e.secondPartyUsername == receiver)
            .toList();
      }
      if (chatss.isNotEmpty) {
        var chat = chatss.first;
        if (chat.messages == null) {
          chat.messages = [];
        }
        chat.messages!.add(Message.fromMap(jsonDecode(res.body)));
        chat.messagesFetched = true;
        chats = chats.where((e) => e.chatId != chat.chatId).toList();
        chats.add(chat);
        chats.sort(
          (a, b) => a.lastMessageDate!.compareTo(b.lastMessageDate!),
        );
        messageController.clear();
        notifyListeners();
      }
    }
  }

  getChats() async {
    final res = await API.apis.getUserChats(loggedUser.username ?? "");
    log("message");
    if (res.statusCode == 200) {
      log(res.body);
      final data = jsonDecode(res.body) as List;

      chats = data.map((e) => Chat.fromMap(e)).toList();
      chats.sort(
        (a, b) => a.lastMessageDate!.compareTo(b.lastMessageDate!),
      );
      notifyListeners();
    }
  }

  getChatMessages(int chatId) async {
    var chatss = chats.where((e) => e.chatId == chatId).toList();
    if (chatss.isNotEmpty) {
      var chat = chatss.first;
      if (chat.messagesFetched) {
        return true;
      } else {
        final res =
            await API.apis.getChatMessages(loggedUser.username ?? "", chatId);
        chat.fillMessages(jsonDecode(res.body));
        chat.messagesFetched = true;
        chats = chats.where((e) => e.chatId != chat.chatId).toList();
        chats.add(chat);
        chats.sort(
          (a, b) => a.lastMessageDate!.compareTo(b.lastMessageDate!),
        );
        notifyListeners();
      }
    }
  }

  fillControllers() {
    usernameController.text = loggedUser.username ?? "";
    emailController.text = loggedUser.email ?? "";
    phoneController.text = loggedUser.phoneNumber ?? "";
    passwordController.text = loggedUser.password ?? "";
    nameController.text = loggedUser.name ?? "";
  }

  setCategory(String label) {
    selectedCategory = label;
    if (selectedCategory == "General") {
      filteredDoctors = doctors.toList();
    } else {
      filteredDoctors = doctors
          .where((element) => element.specialization == selectedCategory)
          .toList();
    }
    notifyListeners();
  }

  late Doctor selectedDoctor;
  XFile? ximageFile;
  File? imageFile;
  chooseImage() async {
    ximageFile = await ImagePicker().pickImage(source: ImageSource.gallery);
    if (ximageFile != null) {
      imageFile = File(ximageFile!.path);
      if (imageFile != null) {
        await API.apis.uploadImage(imageFile!, loggedUser.username ?? "");
        // imageFile = null;
        // ximageFile = null;
      }
    }
    notifyListeners();
  }

  setSearchText(String? x) {
    searchText = x ?? "";

    log(searchText);
    notifyListeners();
  }

  doctorSignup(Map<String, dynamic> map) async {
    final res = await API.apis.doctorSignup(map);
    if (res == null) {
      return false;
    }
    log(res.statusCode.toString());
    if (res.statusCode == 200) {
      loggedUser = User.fromJson(res.body);
      await API.apis.setUserType(loggedUser.username!, userType);
      await getChats();
      if (loggedUser.personType == "Doctor") {
        await getAAPointments();
        await getPAPointments();
      }
      switch (userType) {
        case 0:
          {
            loggedUser.personType = "Doctor";
          }
        case 1:
          {
            loggedUser.personType = "Patient";
          }
        case 2:
          {
            loggedUser.personType = "Nurse";
          }
        case 3:
          {
            loggedUser.personType = "Mother";
          }
      }
      log("message");
      return true;
    }
    return false;
  }

  login(Map<String, dynamic> map) async {
    final res = await API.apis.signin(map);
    if (res == null) {
      return null;
    }
    if (res.statusCode == 200) {
      loggedUser = User.fromJson(res.body);
      await getChats();
      await getNots();
      isLogged = true;
      if (loggedUser.personType == "Doctor") {
        await getAAPointments();
        await getPAPointments();
      } else if (loggedUser.personType == 'Mother') {
        await getChildren();
      }
      fillControllers();
      return true;
    }
  }

  getProfileFile() async {}
  int userType = 0;

  getDoctors() async {
    final res = await API.apis.getDoctors();
    if (res.statusCode == 200) {
      List data = jsonDecode(res.body);
      doctors = data.map((e) => Doctor.fromMap(e)).toList();
      filteredDoctors = doctors.toList();
      notifyListeners();
    }
    log(doctors.length.toString());
  }

  updateUser() async {
    final res = await API.apis.updateUser(<String, dynamic>{
      'username': loggedUser.username,
      'name': nameController.text,
      'phoneNumber': phoneController.text,
      'email': emailController.text,
      'password': passwordController.text
    });
    if (res != null) {
      if (res.statusCode == 200) {
        loggedUser.email = emailController.text;
        loggedUser.name = nameController.text;
        loggedUser.phoneNumber = phoneController.text;
        loggedUser.password = passwordController.text;
        notifyListeners();
      }
    }
  }

  getAAPointments() async {
    final res = await API.apis.getAAppointment(loggedUser.username ?? "");
    if (res != null) {
      if (res.statusCode == 200) {
        List data = jsonDecode(res.body);
        var raappointments = (data).map((e) => Appointment.fromMap(e)).toList();
        aappointments = raappointments.toList();
      }
    }
  }

  getUserAvaila(String username) async {
    final res = await API.apis.getAvail(username);
    if (res != null) {
      if (res.statusCode == 200) {
        Map data = jsonDecode(res.body);
        avs = (data['avaliabilities'] as List)
            .map((e) => Available.fromMap(e))
            .toList();
        aps = (data['appointments'] as List)
            .map((e) => Appointment.fromMap(e))
            .toList();
        log(aps.last.date.toString());
      }
    }
  }

  changeApStatus(int id, int st) async {
    final res = await API.apis.changeAppointmentStatus(
        <String, dynamic>{'appointmentId': id, 'appointmentStatus': st});

    log(res.body);
    log(res.statusCode.toString());
    if (res != null) {
      if (res.statusCode == 200) {
        switch (st) {
          case 1:
            {
              final ap = pappointments
                  .firstWhere((element) => element.appointmentId == id);
              pappointments
                  .removeWhere((element) => element.appointmentId == id);
              ap.status = 'Accepted';
              aappointments.add(ap);
              notifyListeners();
            }
          case 4:
            {
              final ap = pappointments
                  .firstWhere((element) => element.appointmentId == id);
              pappointments
                  .removeWhere((element) => element.appointmentId == id);
              ap.status = 'Canceled';
              // aappointments.add(ap);
              notifyListeners();
            }
        }
      }
    }
  }
  
  

  getUnverifiedDoctors() async {
    var unverifiedDoctors = await API.apis.getUnverifiedDoctors();
    if (unverifiedDoctors.statusCode != 200)
      throw Exception("something went wrong in getUnverifiedDoctors");
    return jsonDecode(unverifiedDoctors.body) as List<dynamic>;
  }

  

  verifyDoctors(String username) async {
    var res = await API.apis.verifyDoctors(username);
    return res.statusCode == 200;
  }

  rejectDoctor(String username) async {
    var res = await API.apis.rejectDoctors(username);
    return res.statusCode == 200;
  }

  getDoctorCredientials() async {
    var doctorCredientials =
        await API.apis.getDoctorCredientials(loggedUser.username!);
    if (doctorCredientials.statusCode != 200)
      throw Exception("something went wrong in getDoctorCredientials");
    return jsonDecode(doctorCredientials.body) as List<dynamic>;
  }

  deleteDoctorCredientials(String id) async {
    var deleteCredientials = await API.apis.deleteDoctorCredientials(id);
    return deleteCredientials.statusCode == 200;
  }

  uploadDoctorCrediential(String fileName, String base64File) async {
    var uploadResult = await API.apis
        .uploadDoctorCrediential(loggedUser.username!, fileName, base64File);
    return uploadResult.statusCode == 200;
  }
  getPAPointments() async {
    final res = await API.apis.getPAppointment(loggedUser.username ?? "");
    if (res != null) {
      if (res.statusCode == 200) {
        List data = jsonDecode(res.body);
        var raappointments = (data).map((e) => Appointment.fromMap(e)).toList();
        pappointments = raappointments.toList();
      }
    }
  }
  
}
