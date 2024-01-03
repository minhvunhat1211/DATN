<template>
    <div class="t-dialog" v-show="isShowSemesterInfor">
         <div class="t-schoolyearinfor" >
            <div class="box-x" @click="clickBtnX()">
                <div class="icon-x"></div>
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập tên học kì</div>
                <input ref="semestername" type="text" class="schoolyearinfor-input" placeholder="Nhập tên học kì" v-model="semester.SemesterName">
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập ngày bắt đầu</div>
                <input type="date" class="schoolyearinfor-input" :value="fomatDate(semester.DateStart)" @input="semester.DateStart = $event.target.value">
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập ngày kết thúc</div>
                <input type="date" class="schoolyearinfor-input" :value="fomatDate(semester.DateEnd)" @input="semester.DateEnd = $event.target.value">
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Chọn năm học</div>
                <div class="box-size box-student-combobox">
                    <input type="text" class="box-size-input box-student-input" readonly :value="semester.SchoolYearName">
                    <div class="box-select-size box-student-select" @click="clickBtnSelectSchoolYear()">
                        <div class="icon-down"></div>
                    </div>
                    <div class="box-size-item box-item-student type-2" v-show="isShowBoxItemSchoolYear">
                        <div v-for="schoolyear in schoolyears" :key="schoolyear.SchoolYearId" class="item-size item-student" @click="clickItemSchoolYear(schoolyear.SchoolYearId, schoolyear.SchoolYearName)">{{schoolyear.SchoolYearName}}</div>
                    </div>
                </div>
            </div>
            <div class="box-schoolyearinfor-bottom">
                <div class="box-schoolyearinfor-cancle" @click="clickBtnCancel()">Hủy</div>
                <div class="box-btn-schoolyearinfor">
                    <div class="box-schoolyearinfor-save" @click="clickBtnSave()">Lưu</div>
                    <div class="box-schoolyearinfor-saveandinsert" v-show="isShowBtnSaveAndInsert" @click="clickBtnSaveAndInsert()">Lưu và thêm</div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import axios from 'axios';
import { eventBus } from '../../main'
import { mapGetters } from 'vuex'
import moment from 'moment'
export default {
    data() {
        return {
            isShowSemesterInfor: false,
            isShowBtnSaveAndInsert: true,
            semester: {},
            schoolyears: [],
            isShowBoxItemSchoolYear: false,
            m2: null,
            formod: null,
        }
    },
    computed: {
        ...mapGetters(['user', 'URLAPI', 'token']),
    },
    methods: {
       /**
        * click Btn hủy
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnCancel(){
           this.isShowSemesterInfor = !this.isShowSemesterInfor;
       },
       /**
        * click Btn X
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnX(){
           this.isShowSemesterInfor = !this.isShowSemesterInfor;
       },
       /**
        * click Btn lưu
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnSave()
       {
            var m = this;
            if(m.formod == "add")
            {
                m.semester.CreatedBy = m.user.UserName;
                axios({
                    method: "post",
                    url: `${m.URLAPI}/api/v1/Semesters`,
                    headers: { 'Authorization': 'Bearer ' + m.token},
                    data: m.semester,
                })
                .then(function (response) {
                    if(response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                    }
                    else{
                        m.semester = {};
                        m.m2.loadDataSemester();
                        // gửi dữ liệu sang component FormToastMessage là 'true'
                        eventBus.$emit("isShowToastMessageWas", true);
                        // gửi dữ liệu sang component FormToastMessage là 'Thêm học kì thành công'
                        eventBus.$emit("TitleToastMessageWas", "Thêm học kì thành công");
                        m.isShowSemesterInfor = !m.isShowSemesterInfor;
                        console.log(response);
                    }
                })
                .catch(function (error) {
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    // gửi dữ liệu đến component FormWaning 
                    eventBus.$emit("errorWas", `${error}`);
                });
            }
            else if(m.formod == "edit")
            {
                m.semester.ModifiedBy = m.user.UserName;
                axios({
                    method: "put",
                    url: `${m.URLAPI}/api/v1/Semesters/${m.semester.SemesterId}`,
                    headers: { 'Authorization': 'Bearer ' + m.token},
                    data: m.semester,
                })
                .then(function (response) {
                    if(response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                    }
                    else{
                        m.semester = {};
                        m.m2.loadDataSemester();
                        // gửi dữ liệu sang component FormToastMessage là 'true'
                        eventBus.$emit("isShowToastMessageWas", true);
                        // gửi dữ liệu sang component FormToastMessage là 'Sửa học kì thành công'
                        eventBus.$emit("TitleToastMessageWas", "Sửa học kì thành công");
                        m.isShowSemesterInfor = !m.isShowSemesterInfor;
                        console.log(response);
                    }
                })
                .catch(function (error) {
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    // gửi dữ liệu đến component FormWaning 
                    eventBus.$emit("errorWas", `${error}`);
                });
            }
       },
       /**
        * click Btn lưu và thêm
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnSaveAndInsert()
       {
            var m = this;
            m.semester.CreatedBy = m.user.UserName;
            axios({
                method: "post",
                url: `${m.URLAPI}/api/v1/Semesters`,
                headers: { 'Authorization': 'Bearer ' + m.token},
                data: m.semester,
            })
            .then(function (response) {
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else{
                    m.semester = {};
                    // gửi dữ liệu sang component FormToastMessage là 'true'
                    eventBus.$emit("isShowToastMessageWas", true);
                    // gửi dữ liệu sang component FormToastMessage là 'Thêm học kì thành công'
                    eventBus.$emit("TitleToastMessageWas", "Thêm học kì thành công");
                    m.m2.loadDataSemester();
                    m.$refs.semestername.focus();
                    console.log(response);
                }
            })
            .catch(function (error) {
                // gửi dữ liệu đến component FormWaning là 'true'
                eventBus.$emit("isShowFormWaningWas", true);
                // gửi dữ liệu đến component FormWaning 
                eventBus.$emit("errorWas", `${error}`);
            });
       },
       /**
        * click Btn lựa chọn năm học
        * CreatedBy: MinhVN(07/03/2022)
        */
        clickBtnSelectSchoolYear(){
            this.isShowBoxItemSchoolYear = !this.isShowBoxItemSchoolYear;
        },
        /**
        * click vào 1 năm học
        * CreatedBy: MinhVN(07/03/2022)
        */
        clickItemSchoolYear(schoolyearId, schoolyearName){
            this.semester.SchoolYearId = schoolyearId;
            this.semester.SchoolYearName = schoolyearName;
            this.isShowBoxItemSchoolYear = !this.isShowBoxItemSchoolYear;
        },
        loadDataSchoolYear(){
           var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/SchoolYears`, 
                {'headers': { 'Authorization': 'Bearer ' + m.token}})
            .then(function(response){
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else
                {
                    m.schoolyears = response.data.Data;
                }
            })
            .catch(function(res){
                console.log(res);
            });
       },
       /**
         * Định dạng lại ngày sinh để hiển thị 
         * CreatedBy: MinhVN (10/02/2022)
         */
        fomatDate(value){
            if (value){
                return moment(String(value)).format('YYYY-MM-DD');
            }
        }
    },
    created() {
        var  m = this;
        m.loadDataSchoolYear();
        eventBus.$on("isShowSemesterInforWas", (isShowSemesterInforData) =>{
            m.isShowSemesterInfor = isShowSemesterInforData;
        });
        eventBus.$on("isShowBtnSaveAndInsertWas", (isShowBtnSaveAndInsertData) =>{
            m.isShowBtnSaveAndInsert = isShowBtnSaveAndInsertData;
        });
        eventBus.$on("thisWas", (thisData) =>{
            m.m2 = thisData;
        });
        eventBus.$on("formodWas", (formodData) =>{
            m.formod = formodData;
        });
        eventBus.$on("semesterWas", (semesterData) =>{
            m.semester = semesterData;
        });
    },
}
</script>