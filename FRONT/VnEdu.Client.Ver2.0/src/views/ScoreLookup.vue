<template>
    <div class="t-scorelookup">
        <div class="header-scorelookup">
            <div class="box-scorelookup">
                <div class="combobox-scorelookup">
                    <div class="name-combobox">Chọn năm học</div>
                    <div class="box-combobox">
                        <div class="box-combobox-header" @click="clickBtnDownSchoolYear()">
                            <input class="input-data" type="text" readonly="true" :value="schoolyearName">
                            <div class="box-icon">
                                <div class="icon-down"></div>
                            </div>
                        </div>
                        <div class="box-combobox-content" v-show="isShowSelectSchoolYear">
                            <div v-for="schoolyear in schoolyears" :key="schoolyear.SchoolYearId" class="item-combobox" @click="clickItemSelectSchoolYear(schoolyear.SchoolYearId, schoolyear.SchoolYearName)">{{schoolyear.SchoolYearName}}</div>
                        </div>
                    </div>
                </div>
                <div class="combobox-scorelookup">
                    <div class="name-combobox">Chọn học kì</div>
                    <div class="box-combobox">
                        <div class="box-combobox-header" @click="clickBtnDownSemester()">
                            <input class="input-data" type="text" readonly="true" :value="semesterName">
                            <div class="box-icon">
                                <div class="icon-down"></div>
                            </div>
                        </div>
                        <div class="box-combobox-content" v-show="isShowSelectSemester">
                            <div v-for="semester in semesters" :key="semester.SemesterId" class="item-combobox" @click="clickItemSelectSemester(semester.SemesterId, semester.SemesterName)">{{semester.SemesterName}}</div>
                        </div>
                    </div>
                </div>
                <div class="combobox-scorelookup">
                    <div class="name-combobox">Chọn lớp học</div>
                    <div class="box-combobox">
                        <div class="box-combobox-header" @click="clickBtnDownClass()">
                            <input class="input-data" type="text" readonly="true" :value="className">
                            <div class="box-icon">
                                <div class="icon-down"></div>
                            </div>
                        </div>
                        <div class="box-combobox-content" v-show="isShowSelectClass">
                            <div v-for="Class in Classs" :key="Class.ClassId" class="item-combobox" @click="clickItemSelectClass(Class.ClassId, Class.ClassName)">{{Class.ClassName}}</div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-see">
                <button class="btn-see" @click="clickBtnSee()">Xem</button>
            </div>
        </div>
        <div class="loading-score" v-show="isShowLoading">
           <pulse-loader :loading="loading" :color="color" :size="size"></pulse-loader>
        </div>
        <div class="bottom-scorelookup" v-show="isShowScore">
            <table class="m-table" border="1">
                <thead>
                    <tr>
                        <th class="text-align-left">TÊN MÔN HỌC</th>
                        <th class="text-align-center" style="width: 120px">ĐIỂM MIỆNG</th>
                        <th class="text-align-center" style="width: 120px">ĐIỂM 15 PHÚT</th>
                        <th class="text-align-center" style="width: 120px">ĐIỂM 1 TIẾT</th>
                        <th class="text-align-center" style="width: 120px">ĐIỂM HỌC KÌ</th>
                        <th class="text-align-center" style="width: 200px">ĐIỂM TRUNG BÌNH MÔN</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="score in scores" :key="score.SubjectId">
                        <th class="text-align-left">{{score.SubjectName}}</th>
                        <th class="text-align-center">{{score.FirstOralScore}}   {{score.SecondOralScore}}   {{score.ThirdOralScore}}</th>
                        <th class="text-align-center">{{score.First15minutesScore}}   {{score.Second15minutesScore}}   {{score.Third15minutesScore}}</th>
                        <th class="text-align-center">{{score.OnePeriodScore}}</th>
                        <th class="text-align-center">{{score.FinalScore}}</th>
                        <th class="text-align-center">{{score.AvgSubject}}</th>
                    </tr>
                </tbody>
            </table>
            <div class="box-score">
                <div class="score-name">{{titleSemester}}</div>
                <div class="score-medium">{{AvgSemeter}}</div>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import { mapGetters } from 'vuex';
import { eventBus } from '../main';
import PulseLoader from 'vue-spinner/src/PulseLoader.vue'
export default {
    components:{
        PulseLoader
    },
    data() {
        return {
            loading: null,
            color: null,
            size: null,
            isShowSelectSemester: false,
            isShowSelectClass: false,
            isShowSelectSchoolYear: false,
            schoolyears: [
               
            ],
            semesters: [
               
            ],
            Classs: [],
            classId: null,
            className: null,
            semesterName: null,
            semesterId: null,
            schoolyearName: null,
            schoolyearId: null,
            scores: [

            ],
            isShowLoading: false,
            isShowScore: false,
            titleSemester: null,
            student: {},
            AvgOralScore: null,
            Avg15minutesScore: null,
            AvgSubject: null,
            count: 0,
            AvgSemeter: 0,
        }
    },

    computed:{
        ...mapGetters(['user', 'URLAPI', 'token']),
    },
    methods: {
        /**
         * click vào btn down gender
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickBtnDownSemester(){
            if(this.schoolyearId == null)
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Vui lòng chọn năm học trước");
            }
            else{
                this.isShowSelectSemester = !this.isShowSelectSemester;
            }
        },

        /**
         * click vào item gender
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickItemSelectSemester(semesterId, semesterName)
        {
            var m = this;
            this.isShowSelectSemester = !this.isShowSelectSemester;
            this.semesterName = semesterName;
            this.semesterId = semesterId;
            axios
            .get(`${m.URLAPI}/api/v1/Student_Classs/GetClassByStudentSchooYearSemester?studentId=${m.student.StudentId}&schoolYearId=${m.schoolyearId}&semesterId=${m.semesterId}`,
                {'headers': { 'Authorization': 'Bearer ' + m.token}})
            .then(function (response){
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    // gửi dữ liệu đến component FormWaning là 'Tên tài khoản hoặc mật khẩu không chính xác'
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else
                { 
                    m.Classs = response.data.Data;
                    if(m.Classs.length == 0)
                    {
                        eventBus.$emit("isShowFormWaningWas", true);
                        eventBus.$emit("errorWas", "Năm học hoặc học kì này học sinh chưa học, vui lòng chọn năm học hoặc học kì khác");
                    }
                }
            })
            .catch(function (res){
                console.log(res);
            });

        },
        /**
         * click vào btn down gender
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickBtnDownClass(){
            if(this.semesterId == null)
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Vui lòng chọn học kì trước");
            }
            else{
                this.isShowSelectClass = !this.isShowSelectClass;
            }
            
        },

        /**
         * click vào item gender
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickItemSelectClass(classId, className)
        {

            this.isShowSelectClass = !this.isShowSelectClass;
            this.className = className;
            this.classId = classId;

        },
        /**
         * click vào btn down gender
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickBtnDownSchoolYear(){
            this.isShowSelectSchoolYear = !this.isShowSelectSchoolYear;
        },

        /**
         * click vào item gender
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickItemSelectSchoolYear(schoolyearId, schoolyearName)
        {
            this.isShowSelectSchoolYear = !this.isShowSelectSchoolYear;
            this.schoolyearName = schoolyearName;
            this.schoolyearId = schoolyearId;

            this.getDataSemester(schoolyearId);
        },

        /**
         * lấy tất cả năm học
         * CreatedBy: MinhVN(25/01/2022)
         */
        getDataSchoolYear(){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/SchoolYears`, 
                {'headers': { 'Authorization': 'Bearer ' + m.token}})
            .then(function (response){
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    // gửi dữ liệu đến component FormWaning là 'Tên tài khoản hoặc mật khẩu không chính xác'
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else
                { 
                   m.schoolyears = response.data.Data;
                }
            })
            .catch(function (res){
                console.log(res);
            });
        },

        /**
         * lấy tất cả học kì
         * CreatedBy: MinhVN(25/01/2022)
         */
        getDataSemester(schoolYearId){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Semesters/GetSemesterBySchoolYearId?schoolYearId=${schoolYearId}`,
                {'headers': { 'Authorization': 'Bearer ' + m.token}})
            .then(function (response){
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    // gửi dữ liệu đến component FormWaning là 'Tên tài khoản hoặc mật khẩu không chính xác'
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else
                { 
                   m.semesters = response.data.Data;
                }
            })
            .catch(function (res){
                console.log(res);
            });
        },
        /**
         * click vào btn xem
         * CreatedBy: MinhVN(25/01/2022)
         */
        clickBtnSee(){
            var m = this;
            if(m.schoolyearId == null || m.schoolyearId == "")
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Vui lòng chọn năm học cần xem");
            }
            else if(m.semesterId == null || m.semesterId == "")
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Vui lòng chọn học kì cần xem");
            }
            else if(m.classId == null || m.classId == "")
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Vui lòng chọn lớp học cần xem");
            }
            else if(m.Classs.length == 0)
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Năm học hoặc học kì này học sinh chưa học, vui lòng chọn năm học hoặc học kì khác");
            }
            else{
                m.isShowScore = !m.isShowScore;
                m.isShowLoading = !m.isShowLoading;
                axios
                .get(`${m.URLAPI}/api/v1/Student_Subjects/GetScoreByStudent?schoolYearId=${m.schoolyearId}&semesterId=${m.semesterId}&studentId=${m.student.StudentId}`,
                    {'headers': { 'Authorization': 'Bearer ' + m.token}})
                .then(function (response){
                    if(response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        // gửi dữ liệu đến component FormWaning là 'Tên tài khoản hoặc mật khẩu không chính xác'
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                    }
                    else
                    { 
                        m.semesterSum = 0;
                        m.scores = response.data.Data;
                        if(m.semesterName == "Học kì 1")
                        {
                            m.titleSemester = "TRUNG BÌNH HỌC KÌ I";
                        }
                        else if(m.semesterName == "Học kì 2")
                        {
                            m.titleSemester = "TRUNG BÌNH HỌC KÌ II";
                        }
                        for (const i of m.scores) {
                            // tính điểm trung bình miệng
                            if(i.FirstOralScore != null && i.SecondOralScore != null && i.ThirdOralScore == null)
                            {
                                m.AvgOralScore = (((i.FirstOralScore) + (i.SecondOralScore)) / 2).toFixed(2);
                            }
                            else if(i.FirstOralScore != null && i.SecondOralScore == null && i.ThirdOralScore != null)
                            {
                                m.AvgOralScore = (((i.FirstOralScore) + (i.ThirdOralScore)) / 2).toFixed(2);
                            }
                            else if(i.FirstOralScore == null && i.SecondOralScore != null &&i.ThirdOralScore != null)
                            {
                                m.AvgOralScore = (((i.SecondOralScore) + (i.ThirdOralScore)) / 2).toFixed(2);
                            }
                            else if(i.FirstOralScore != null && i.SecondOralScore == null && i.ThirdOralScore == null)
                            {
                                m.AvgOralScore = (i.FirstOralScore).toFixed(2);
                            }
                            else if(i.FirstOralScore == null && i.SecondOralScore != null && i.ThirdOralScore == null)
                            {
                                m.AvgOralScore = (i.SecondOralScore).toFixed(2);
                            }
                            else if(i.FirstOralScore == null && i.SecondOralScore == null && i.ThirdOralScore != null)
                            {
                                m.AvgOralScore = (i.ThirdOralScore).toFixed(2);
                            }
                            else if(i.FirstOralScore != null && i.SecondOralScore != null && i.ThirdOralScore != null)
                            {
                                m.AvgOralScore = (((i.FirstOralScore) + (i.SecondOralScore) + (i.ThirdOralScore)) / 3).toFixed(2);
                            }
                            else if(i.FirstOralScore == null && i.SecondOralScore == null && i.ThirdOralScore == null)
                            {
                                m.AvgOralScore = null;
                            }

                            // tính điểm trung bình 15 phút
                            if(i.First15minutesScore != null && i.Second15minutesScore != null && i.Third15minutesScore == null)
                            {
                                m.Avg15minutesScore = (((i.First15minutesScore) + (i.Second15minutesScore)) / 2).toFixed(2);
                            }
                            else if(i.First15minutesScore != null && i.Second15minutesScore == null && i.Third15minutesScore != null)
                            {
                                m.Avg15minutesScore = (((i.First15minutesScore) + (i.Third15minutesScore)) / 2).toFixed(2);
                            }
                            else if(i.First15minutesScore == null && i.Second15minutesScore != null &&i.Third15minutesScore != null)
                            {
                                m.Avg15minutesScore = (((i.Second15minutesScore) + (i.Third15minutesScore)) / 2).toFixed(2);
                            }
                            else if(i.First15minutesScore != null && i.Second15minutesScore == null && i.Third15minutesScore == null)
                            {
                                m.Avg15minutesScore = (i.First15minutesScore).toFixed(2);
                            }
                            else if(i.First15minutesScore == null && i.Second15minutesScore != null && i.Third15minutesScore == null)
                            {
                                m.Avg15minutesScore = (i.Second15minutesScore).toFixed(2);
                            }
                            else if(i.First15minutesScore == null && i.Second15minutesScore == null && i.Third15minutesScore != null)
                            {
                                m.Avg15minutesScore = (i.Third15minutesScore).toFixed(2);
                            }
                            else if(i.First15minutesScore != null && i.Second15minutesScore != null && i.Third15minutesScore != null)
                            {
                                m.Avg15minutesScore = (((i.First15minutesScore) + (i.Second15minutesScore) + (i.Third15minutesScore)) / 3).toFixed(2);
                            }
                            else if(i.First15minutesScore == null && i.Second15minutesScore == null && i.Third15minutesScore == null)
                            {
                                m.Avg15minutesScore = null;
                            }
                        }
                        
                        // tính điểm trung bình môn
                        for (const i of m.scores) {
                            if(m.AvgOralScore != null && m.Avg15minutesScore != null && i.OnePeriodScore != null && i.FinalScore != null)
                            {
                                i.AvgSubject = (((m.AvgOralScore)* 0.1) + ((m.Avg15minutesScore) * 0.2) + ((i.OnePeriodScore) * 0.3) + ((i.FinalScore) * 0.4)).toFixed(2);
                            }
                            else
                            {
                                 i.AvgSubject = null;
                            }
                        }
                        m.count = 0;
                        m.AvgSemeter = 0;
                        // tính điểm trung học kì
                        for (const i of m.scores) {
                            if(i.AvgSubject != null)
                            {
                                m.count++;
                                m.AvgSemeter+= parseFloat(i.AvgSubject);
                            }
                        }

                        if(m.count >= 13)
                        {
                            m.AvgSemeter = ((m.AvgSemeter) / 13).toFixed(2);
                        }
                        else{
                            m.AvgSemeter = null;
                        }             

                        m.isShowLoading = false;
                        m.isShowScore = true;
                        eventBus.$emit("isShowToastMessageWas", true);
                        eventBus.$emit("TitleToastMessageWas", "Xem điểm thành công");
                    }
                })
                .catch(function (res){
                    console.log(res);
                });
            }
        },
        LoadDataInformationStudent()
        {
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Students/GetInformationByEmail?email=${m.user.Email}`,
                {'headers': { 'Authorization': 'Bearer ' + m.token }})
            .then(function (response){
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    // gửi dữ liệu đến component FormWaning là 'Tên tài khoản hoặc mật khẩu không chính xác'
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else
                { 
                    m.student = response.data.Data;
                }
            })
            .catch(function (res){
                console.log(res);
            });
        },
    },

    created() {
        var m = this;
        m.getDataSchoolYear();
        m.LoadDataInformationStudent();
    },
}
</script>