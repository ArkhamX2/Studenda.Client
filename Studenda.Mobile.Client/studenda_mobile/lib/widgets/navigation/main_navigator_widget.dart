import 'package:flutter/material.dart';
import 'package:studenda_mobile/widgets/home/home_screen_widget.dart';
import 'package:studenda_mobile/widgets/journal/journal_screen_widget.dart';
import 'package:studenda_mobile/widgets/schedule/schedule_screen_widget.dart';

class MainNavigatorWidget extends StatefulWidget {
  const MainNavigatorWidget({super.key});

  @override
  State<MainNavigatorWidget> createState() =>
      _MainNavigatorWidgetState();
}

class _MainNavigatorWidgetState extends State<MainNavigatorWidget> {
  int _selectedIndex = 0;
  final List<Widget> _widgetOptions = <Widget>[
    const HomeScreenWidget(),
    const ScheduleScreenWidget(),
    const JournalScreenWidget(),
  ];

  void _onItemTap(int index) {
    setState(() {
      _selectedIndex = index;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color.fromARGB(255, 240, 241, 245),
      body: Container(
        child: _widgetOptions.elementAt(_selectedIndex),
      ),
      bottomNavigationBar: BottomNavigationBar(
        items: const <BottomNavigationBarItem>[
          BottomNavigationBarItem(
            icon: Icon(Icons.home),
            label: "Главная",
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.schedule),
            label: "Расписание",
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.book),
            label: "Журнал",
          ),
        ],
        currentIndex: _selectedIndex,
        onTap: _onItemTap,
      ),
    );
  }
}
