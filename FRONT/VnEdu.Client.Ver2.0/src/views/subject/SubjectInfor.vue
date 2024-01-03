<template>
    <div class="t-dialog" v-show="isShowSubjectInfor">
         <div class="t-schoolyearinfor" >
            <div class="box-x" @click="clickBtnX()">
                <div class="icon-x"></div>
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập tên môn học</div>
                <input ref="subjectname" type="text" class="schoolyearinfor-input" placeholder="Nhập tên môn học" v-model="subject.SubjectName">
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Số tiết/năm/lớp</div>
                <input ref="subjectname" type="text" class="schoolyearinfor-input" placeholder="Nhập số tiết học" v-model="subject.PeriodsPerYear">
            </div>
             <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Số đơn vị học phần</div>
                <input ref="subjectname" type="text" class="schoolyearinfor-input" placeholder="Nhập số tiết học" v-model="subject.Credit">
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
            isShowSubjectInfor: false,
            isShowBtnSaveAndInsert: true,
            subject: {},
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
           this.isShowSubjectInfor = !this.isShowSubjectInfor;
       },
       /**
        * click Btn X
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnX(){
           this.isShowSubjectInfor = !this.isShowSubjectInfor;
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
                m.subject.CreatedBy = m.user.UserName;
                axios({
                    method: "post",
                    url: `${m.URLAPI}/api/v1/Subjects`,
                    headers: { 'Authorization': 'Bearer ' + m.token},
                    data: m.subject,
                })
                .then(function (response) {
                    if(response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                    }
                    else{
                        m.subject = {};
                        m.m2.loadDataSubject();
                        // gửi dữ liệu sang component FormToastMessage là 'true'
                        eventBus.$emit("isShowToastMessageWas", true);
                        // gửi dữ liệu sang component FormToastMessage là 'Thêm môn học thành công'
                        eventBus.$emit("TitleToastMessageWas", "Thêm môn học thành công");
                        m.isShowSubjectInfor = !m.isShowSubjectInfor;
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
                m.subject.ModifiedBy = m.user.UserName;
                axios({
                    method: "put",
                    url: `${m.URLAPI}/api/v1/Subjects/${m.subject.SubjectId}`,
                    headers: { 'Authorization': 'Bearer ' + m.token},
                    data: m.subject,
                })
                .then(function (response) {
                    if(response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                    }
                    else{
                        m.subject = {};
                        m.m2.loadDataSubject();
                        // gửi dữ liệu sang component FormToastMessage là 'true'
                        eventBus.$emit("isShowToastMessageWas", true);
                        // gửi dữ liệu sang component FormToastMessage là 'Sửa môn học thành công'
                        eventBus.$emit("TitleToastMessageWas", "Sửa môn học thành công");
                        m.isShowSubjectInfor = !m.isShowSubjectInfor;
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
            m.subject.CreatedBy = m.user.UserName;
            axios({
                method: "post",
                url: `${m.URLAPI}/api/v1/Subjects`,
                headers: { 'Authorization': 'Bearer ' + m.token},
                data: m.subject,
            })
            .then(function (response) {
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else{
                    m.subject = {};
                    // gửi dữ liệu sang component FormToastMessage là 'true'
                    eventBus.$emit("isShowToastMessageWas", true);
                    // gửi dữ liệu sang component FormToastMessage là 'Thêm môn học thành công'
                    eventBus.$emit("TitleToastMessageWas", "Thêm môn học thành công");
                    m.m2.loadDataSubject();
                    m.$refs.subjectname.focus();
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
    created() {
        var  m = this;
        eventBus.$on("isShowSubjectInforWas", (isShowSubjectInforData) =>{
            m.isShowSubjectInfor = isShowSubjectInforData;
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
        eventBus.$on("subjectWas", (subjectData) =>{
            m.subject = subjectData;
        });
    },
}
</script>