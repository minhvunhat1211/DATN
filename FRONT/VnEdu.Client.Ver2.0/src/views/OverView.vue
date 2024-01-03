<template>
    <div class="t-overview">
       <div class="box-overview grade">
           <div class="box-name-overview">
               <div class="header-name-overview">{{schoolyears.length}}</div>
               <div class="bottom-name-overview">Năm học</div>
           </div>
           <div class="box-icon-overview"></div>
       </div>
       <div class="box-overview class">
           <div class="box-name-overview">
               <div class="header-name-overview">{{classs.length}}</div>
               <div class="bottom-name-overview">Lớp</div>
           </div>
           <div class="box-icon-overview"></div>
       </div>
       <div class="box-overview student">
           <div class="box-name-overview">
               <div class="header-name-overview">{{students.length}}</div>
               <div class="bottom-name-overview">Học sinh</div>
           </div>
           <div class="box-icon-overview"></div>
       </div>
       <div class="box-overview subject">
           <div class="box-name-overview">
               <div class="header-name-overview">{{teachers.length}}</div>
               <div class="bottom-name-overview">Giáo viên</div>
           </div>
           <div class="box-icon-overview"></div>
       </div>
    </div>
</template>

<script>
import axios from 'axios'
import { mapGetters } from 'vuex'
import { eventBus } from'../main.js'
export default {
    data() {
        return {
            schoolyears: [],
            classs: [],
            students: [],
            teachers: [],
        }
    },
    computed: {
        ...mapGetters(['URLAPI', 'token']),
    },
    methods: {
        /**
         * lấy tất cả năm học
         * CreatedBy: MinhVN(26/01/2022)
         */
        LoadDataSchoolYears(){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/SchoolYears`, 
                {'headers': { 'Authorization': 'Bearer ' + m.token}})
            .then(function (response){
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
            .catch(function (res){
                console.log(res);
            });
        },
        /**
         * lấy tất cả lớp
         * CreatedBy: MinhVN(26/01/2022)
         */
        LoadDataClasss(){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Classs`,
                {'headers': { 'Authorization': 'Bearer ' + m.token}})
            .then(function (response){
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else
                { 
                   m.classs = response.data.Data;
                }
            })
            .catch(function (res){
                console.log(res);
            });
        },
        /**
         * lấy tất cả học sinh
         * CreatedBy: MinhVN(26/01/2022)
         */
        LoadDataStudents(){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Students`,
                {'headers': { 'Authorization': 'Bearer ' + m.token}})
            .then(function (response){
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else
                { 
                   m.students = response.data.Data;
                }
            })
            .catch(function (res){
                console.log(res);
            });
        },
        /**
         * lấy tất cả giáo viên
         * CreatedBy: MinhVN(26/01/2022)
         */
        LoadDataTeachers(){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Teachers`,
                {'headers': { 'Authorization': 'Bearer ' + m.token}})
            .then(function (response){
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
            .catch(function (res){
                console.log(res);
            });
        },
    },
    created() {
        var m = this;
        m.LoadDataSchoolYears();
        m.LoadDataClasss();
        m.LoadDataStudents();
        m.LoadDataTeachers();
    },
}
</script>