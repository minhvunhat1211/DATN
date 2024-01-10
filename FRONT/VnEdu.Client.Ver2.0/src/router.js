import TheMain from './components/TheMain.vue';
import TheLogin from './components/TheLogin.vue';
import OverView from './views/OverView.vue';
import ChangePassword from './views/ChangePassword.vue';
import StudentInformation from './views/StudentInformation.vue';
import ScoreLookup from './views/ScoreLookup.vue';
import SchoolYear from './views/schoolyear/SchoolYear.vue';
import Semester from './views/semester/Semester.vue';
import Subject from './views/subject/Subject.vue';
import Decentralization from './views/decentralization/Decentralization.vue';
import Class from './views/class/Class.vue';
import Chat from './views/Chat/Chat.vue'
import User from './views/user/User.vue';
import Student from './views/student/Student.vue';
import Teacher from './views/teacher/Teacher.vue';
import StudentClass from './views/studentclass/StudentClass.vue';
import TeacherClass from './views/teacherclass/TeacherClass.vue';
import TeacherSubject from './views/teachersubject/TeacherSubject.vue';
import StudentSubject from './views/studentsubject/StudentSubject.vue';
import DetailedTableScore from './views/detailedtablescore/DetailedTableScore.vue';
import EmailVerification from './views/EmailVerification.vue';
import Verification from './views/Verification.vue';
import ChangePasswordNew from './views/ChangePasswordNew.vue';
import VueRouter from 'vue-router';

const routes = [{
        path: '/',
        component: TheLogin,
        redirect: '/login',
    },
    { path: '/login', component: TheLogin },
    { path: '/emailverification', component: EmailVerification },
    { path: '/verification', component: Verification },
    { path: '/changepasswordnew', component: ChangePasswordNew },
    {
        path: '/main',
        component: TheMain,
        redirect: '/overview',
        children: [
            { path: '/studentinformation', component: StudentInformation },
            { path: '/changepassword', component: ChangePassword },
            { path: '/studentclass', component: StudentClass },
            { path: '/scorelookup', component: ScoreLookup },
            { path: '/schoolyear', component: SchoolYear },
            { path: '/semester', component: Semester },
            { path: '/subject', component: Subject },
            { path: '/class', component: Class },
            { path: '/chat', component: Chat },
            { path: '/user', component: User },
            { path: '/student', component: Student },
            { path: '/teacher', component: Teacher },
            { path: '/overview', component: OverView },
            { path: '/teacherclass', component: TeacherClass },
            { path: '/teachersubject', component: TeacherSubject },
            { path: '/studentsubject', component: StudentSubject },
            { path: '/decentralization', component: Decentralization },
            { path: '/detailedtablescore', component: DetailedTableScore },
        ]
    },
]

export default new VueRouter({
    mode: 'history',
    routes
})