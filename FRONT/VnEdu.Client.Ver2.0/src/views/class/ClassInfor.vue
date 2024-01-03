<template>
    <div class="t-dialog" v-show="isShowClassInfor">
         <div class="t-schoolyearinfor" >
            <div class="box-x" @click="clickBtnX()">
                <div class="icon-x"></div>
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập tên lớp học</div>
                <input ref="classname" type="text" class="schoolyearinfor-input" placeholder="Nhập tên lớp học" v-model="Class.ClassName">
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Chủ nhiệm lớp</div>
                <div class="box-size box-student-combobox">
                    <input type="text" class="box-size-input box-student-input" readonly :value="Class.FullName">
                    <div class="box-select-size box-student-select" @click="clickBtnSelectTeacher()">
                        <div class="icon-down"></div>
                    </div>
                    <div class="box-size-item box-item-student type-2 type-10" v-show="isShowBoxItemTeacher">
                        <div v-for="teacher in teachers" :key="teacher.TeacherId" class="item-size item-student" @click="clickItemTeacher(teacher.TeacherId, teacher.FullName)">{{teacher.FullName}}</div>
                    </div>
                </div>
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập khối học</div>
                <input ref="classname" type="text" class="schoolyearinfor-input" placeholder="Nhập khối học" v-model="Class.Grade">
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập phòng học</div>
                <input ref="classname" type="text" class="schoolyearinfor-input" placeholder="Nhập phòng học" v-model="Class.Room">
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Chọn năm học</div>
                <div class="box-size box-student-combobox">
                    <input type="text" class="box-size-input box-student-input" readonly :value="Class.SchoolYearName">
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
export default {
    data() {
        return {
            isShowClassInfor: false,
            isShowBtnSaveAndInsert: true,
            isShowBoxItemSchoolYear: false,
            isShowBoxItemTeacher: false,
            schoolyears: [],
            teachers: [],
            Class: {},
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
           this.isShowClassInfor = !this.isShowClassInfor;
       },
       /**
        * click Btn X
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnX(){
           this.isShowClassInfor = !this.isShowClassInfor;
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
                m.Class.CreatedBy = m.user.UserName;
                axios({
                    method: "post",
                    url: `${m.URLAPI}/api/v1/Classs`,
                    headers: { 'Authorization': 'Bearer ' + m.token},
                    data: m.Class,
                })
                .then(function (response) {
                    if(response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                    }
                    else{
                        m.Class = {};
                        m.m2.loadDataClass();
                        // gửi dữ liệu sang component FormToastMessage là 'true'
                        eventBus.$emit("isShowToastMessageWas", true);
                        // gửi dữ liệu sang component FormToastMessage là 'Thêm lớp học thành công'
                        eventBus.$emit("TitleToastMessageWas", "Thêm lớp học thành công");
                        m.isShowClassInfor = !m.isShowClassInfor;
                        console.log(response);
                    }
                })
                .catch(function (error) {
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    // gửi dữ liệu đến component FormWaning 
                    eventBus.$emit("errorWas", error);
                });
            }
            else if(m.formod == "edit")
            {
                m.Class.ModifiedBy = m.user.UserName;
                axios({
                    method: "put",
                    url: `${m.URLAPI}/api/v1/Classs/${m.Class.ClassId}`,
                    headers: { 'Authorization': 'Bearer ' + m.token},
                    data: m.Class,
                })
                .then(function (response) {
                    if(response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                    }
                    else{
                        m.CLass = {};
                        m.m2.loadDataClass();
                        // gửi dữ liệu sang component FormToastMessage là 'true'
                        eventBus.$emit("isShowToastMessageWas", true);
                        // gửi dữ liệu sang component FormToastMessage là 'Sửa lớp học thành công'
                        eventBus.$emit("TitleToastMessageWas", "Sửa lớp học thành công");
                        m.isShowClassInfor = !m.isShowClassInfor;
                        console.log(response);
                    }
                })
                .catch(function (error) {
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    // gửi dữ liệu đến component FormWaning 
                    eventBus.$emit("errorWas", error);
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
           m.Class.CreatedBy = m.user.UserName;
           axios({
                method: "post",
                url: `${m.URLAPI}/api/v1/Classs`,
                headers: { 'Authorization': 'Bearer ' + m.token},
                data: m.Class,
            })
            .then(function (response) {
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else{
                    m.Class = {};
                    // gửi dữ liệu sang component FormToastMessage là 'true'
                    eventBus.$emit("isShowToastMessageWas", true);
                    // gửi dữ liệu sang component FormToastMessage là 'Thêm lớp học thành công'
                    eventBus.$emit("TitleToastMessageWas", "Thêm lớp học thành công");
                    m.m2.loadDataClass();
                    m.$refs.classname.focus();
                    console.log(response);
                }
            })
            .catch(function (error) {
                // gửi dữ liệu đến component FormWaning là 'true'
                eventBus.$emit("isShowFormWaningWas", true);
                // gửi dữ liệu đến component FormWaning 
                eventBus.$emit("errorWas", error);
            });
       },
        /**
        * click vào 1 năm học
        * CreatedBy: MinhVN(07/03/2022)
        */
        clickItemSchoolYear(schoolyearId, schoolyearName){
            this.Class.SchoolYearId = schoolyearId;
            this.Class.SchoolYearName = schoolyearName;
            this.isShowBoxItemSchoolYear = !this.isShowBoxItemSchoolYear;
        },
         /**
        * click vào 1 giáo viên
        * CreatedBy: MinhVN(07/03/2022)
        */
        clickItemTeacher(teacherId, fullName){
            this.Class.TeacherId = teacherId;
            this.Class.FullName = fullName;
            this.isShowBoxItemTeacher = !this.isShowBoxItemTeacher;
        },
        /**
        * click Btn lựa chọn năm học
        * CreatedBy: MinhVN(07/03/2022)
        */
        clickBtnSelectSchoolYear(){
            this.isShowBoxItemSchoolYear = !this.isShowBoxItemSchoolYear;
        },
        /**
        * click Btn lựa chọn giáo viên
        * CreatedBy: MinhVN(07/03/2022)
        */
        clickBtnSelectTeacher(){
            this.isShowBoxItemTeacher = !this.isShowBoxItemTeacher;
        },
        /**
        * lấy tất cả năm học
        * CreatedBy: MinhVN(07/03/2022)
        */
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
        * lấy tất cả giáo viên
        * CreatedBy: MinhVN(07/03/2022)
        */
        loadDataTeacher(){
           var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Teachers`, 
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
                    m.teachers = response.data.Data;
                }
            })
            .catch(function(res){
                console.log(res);
            });
       }
    },
    created() {
        var  m = this;
        m.loadDataSchoolYear();
        m.loadDataTeacher();
        eventBus.$on("isShowClassInforWas", (isShowClassInforData) =>{
            m.isShowClassInfor = isShowClassInforData;
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
        eventBus.$on("classWas", (classData) =>{
            m.Class = classData;
        });
    },
}
</script>