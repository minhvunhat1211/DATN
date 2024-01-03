<template>
    <div class="t-schoolyear">
        <div class="header-schoolyear">
            <div class="header-name">Thông tin giáo viên</div>
            <div class="header-btn-insert" @click="clickBtnInsert()">Thêm giáo viên mới</div>
        </div>
        <div class="box-search box-excel">
            <div class="box-input-text input-student">
                <input type="text" class="input-text" placeholder="Tìm kiếm theo mã, tên giáo viên, số điện thoại" v-model="SearchValue">
                <div class="box-icon-search"  @click="clickBtnSearch()">
                    <div class="icon-search"></div>
                </div>
            </div>
            <div class="export-excel" @click="clickBtnExportExcel()"></div>
        </div>
        <div class="between-schoolyear table-user">
            <table class="m-table" border="1">
                <thead>
                    <tr>
                        <th class="text-align-center" style="width: 50px">STT</th>
                        <th class="text-align-left" style="width: 110px">Mã giáo viên</th>
                        <th class="text-align-left">Tên giáo viên</th>
                        <th class="text-align-center" style="width: 80px">Giới tính</th>
                        <th class="text-align-center" style="width: 90px">Ngày sinh</th>
                        <th class="text-align-center" style="width: 110px">Số điện thoại</th>
                        <th class="text-align-left">Email</th>
                        <th class="text-align-center">Sô CMND/CCCD</th>
                        <th class="text-align-left">Địa chỉ hiện tại</th>
                        <th class="text-align-left">Quê quán</th>
                        <th class="text-align-center" style="width: 120px">Chức năng</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(teacher,i) in teachers" :key="teacher.TeacherId">
                        <th class="text-align-center">{{++i}}</th>
                        <th class="text-align-left">{{teacher.TeacherCode}}</th>
                        <th class="text-align-left">{{teacher.FullName}}</th>
                        <th class="text-align-center">{{FomatGender(teacher.Gender)}}</th>
                        <th class="text-align-center">{{FomatDate(teacher.DateOfBirth)}}</th>
                        <th class="text-align-center">{{teacher.PhoneNumber}}</th>
                        <th class="text-align-left">{{teacher.Email}}</th>
                        <th class="text-align-center">{{teacher.NumberCard}}</th>
                        <th class="text-align-left">{{teacher.AddressCurent}}</th>
                        <th class="text-align-left">{{teacher.Address}}</th>
                        <th class="text-align-center">
                            <div class="box-function">
                                <div class="btn-edit" @click="clickBtnEdit(teacher.TeacherId)"></div>
                                <div class="btn-delete" @click="clickBtnDelete(teacher.TeacherId, teacher.TeacherCode)"></div>
                            </div>
                        </th>
                    </tr>  
                </tbody>
            </table>
        </div>
        <div class="bottom-schoolyear box-type-paging">
            <div class="box-sum">Tổng số: {{teachers.length}} bản ghi</div>
            <div class="box-paging">
                <div class="pri" @click="clickPagePri()">
                    <div class="page-pri"></div>
                </div>
                <div class="page-number">
                    <div v-for="i in pagings" :key="i" :class="{ active: pageNumber == i }" class="number" @click="clickPageNumber(i)" >{{i}}</div>
                </div>
                <div class="next" @click="clickPageNext()">
                    <div class="page-next" ></div>
                </div>
            </div>
            <div class="box-size">
                <input type="text" class="box-size-input" readonly :value="pagename">
                <div class="box-select-size" @click="clickBtnSelectSize()">
                    <div class="icon-size"></div>
                </div>
                <div class="box-size-item" v-show="isShowBoxItemSize">
                    <div v-for="page in pages" :key="page.index" class="item-size" @click="clickItemSize(page.index, page.name)">{{page.name}}</div>
                </div>
            </div>
        </div>
       <teacher-infor></teacher-infor>
       <delete-teacher></delete-teacher>
    </div>
</template>
<script>
import * as XLSX from 'xlsx'
import axios from 'axios'
import { eventBus } from '../../main'
import TeacherInfor from './TeacherInfor.vue'
import DeleteTeacher from './DeleteTeacher.vue'
import moment from 'moment'
import { mapGetters } from 'vuex'
export default {
    components: { TeacherInfor, DeleteTeacher },

    data() {
        return {
            teachers: [],
            pages: [
                {
                    index: 10,
                    name: "10 bản ghi trên 1 trang"
                },
                {
                    index: 20,
                    name: "20 bản ghi trên 1 trang"
                },
                {
                    index: 30,
                    name: "30 bản ghi trên 1 trang"
                },
                {
                    index: 50,
                    name: "50 bản ghi trên 1 trang"
                },
                {
                    index: 100,
                    name: "100 bản ghi trên 1 trang"
                }
            ],
            isShowBoxItemSize: false,
            isShowBoxItemGrade: false,
            isShowBoxItemClass: false,
            grades: [],
            classs: [],
            gradename: null,
            classname: null,
            SearchValue: '',
            pageSize: 10, 
            pageNumber: 1,
            totalRecord: null,
            totalPage: null,
            pagename: "10 bản ghi trên 1 trang"
        }
    },
    computed:{
        ...mapGetters(['URLAPI', 'token']),
        pagings() {
            let numShown = 5;
            numShown = Math.min(numShown, this.totalPage);
            let first = this.pageNumber - Math.floor(numShown / 2);
            first = Math.max(first, 1);
            first = Math.min(first, this.totalPage - numShown + 1);
            return [...Array(numShown)].map((k,i) => i + first);
        }
    },
    methods: {
        clickPageNumber(i){
            var m = this;
            m.pageNumber = i;
            m.loadDataTeacher();
        },
        clickPageNext(){
            var m = this;
            if(m.pageNumber < m.totalPage)
            {
                m.pageNumber++;
                m.loadDataTeacher();
                console.log(m.pageNumber);
            }
            else
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Đã đến trang cuối cùng");
            }
        },
        clickPagePri(){
            var m = this;
            if(m.pageNumber > 1)
            {
                m.pageNumber--;
                m.loadDataTeacher();
                console.log(m.pageNumber);
            }
            else
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Đã đến trang đầu tiên");
            }
        },
        /**
         * click vào btn tìm kiếm 
         * CreatedBy: MinhVN (04/01/2022)
         */
        clickBtnSearch(){
            this.pageNumber = 1;
            this.loadDataTeacher();
            eventBus.$emit("isShowToastMessageWas", true);
            eventBus.$emit("TitleToastMessageWas", "Tìm kiếm giáo viên thành công");
        },
        /**
         * Định dạng lại ngày sinh để hiển thị 
         * CreatedBy: MinhVN (09/02/2022)
         */
        FomatDate(value){
            if (value){
                return moment(String(value)).format('DD-MM-YYYY');
            }
        },
        /**
         * Định dạng lại giới tính 
         * CreatedBy: MinhVN (09/02/2022)
         */
        FomatGender(value){
            if (value != null){
                if(value == 0)
                {
                    return "Nữ";
                }
                else if(value == 1)
                {
                    return "Nam";
                }
                else if(value == 2){
                    return "Khác";
                }
            }
            else{
                return "";
            }
        },
        /**
         * click vào btn hiện box size
         * CreatedBy: MinhVN(08/02/2022)
         */
        clickBtnSelectSize(){
            this.isShowBoxItemSize = !this.isShowBoxItemSize;
        },
        /**
         * click vào btn item page 
         * CreatedBy: MinhVN (05/01/2021)
         */
        clickItemSize(index, name){
            this.pageSize = index;
            this.pagename = name;
            this.loadDataTeacher();
            this.isShowBoxItemSize = false;
        },
        /**
         * click vào btn thêm mới người dùng
         * CreatedBy: MinhVN(08/02/2022)
         */
        clickBtnInsert(){
            eventBus.$emit("isShowBtnSaveAndInsertWas", true);
            eventBus.$emit("isShowTeacherInforWas", true);
            eventBus.$emit('formodWas', "add");
            eventBus.$emit('thisWas', this);
            eventBus.$emit('teacherWas', {});
            eventBus.$emit('gradesWas', this.grades);
             eventBus.$emit('nullWas', null);
        },
        /**
         * Lấy tất cả teacher
         * CreatedBy: MinhVN(08/02/2022)
         */
        loadDataTeacher(){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Teachers/GetPagingTeacher?valueFilter=${m.SearchValue}&pageIndex=${m.pageNumber}&pageSize=${m.pageSize}`,
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
                    m.teachers = response.data.Data.Data;
                    m.totalRecord = response.data.Data.ToTalRecord;
                    m.totalPage = response.data.Data.ToTalPage;
                }
            })
            .catch(function(res){
                console.log(res);
            });
        },
        /**
         * click btn sửa
         * CreatedBy: MinhVN(08/02/2022)
         */
        clickBtnEdit(teacherId){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Teachers/${teacherId}`,
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
                    eventBus.$emit('teacherWas', response.data.Data);
                } 
            })
            .catch(function (res){
                // gửi dữ liệu đến component FormWaning là 'true'
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", `${res}`);
            });
            eventBus.$emit("isShowTeacherInforWas", true);
            eventBus.$emit("isShowBtnSaveAndInsertWas", false);
            eventBus.$emit('formodWas', "edit");
            eventBus.$emit('thisWas', this);
        },
        /**
         * click btn xóa
         * CreatedBy: MinhVN(08/02/2022)
         */
        clickBtnDelete(teacherId, teacherCode){
            eventBus.$emit('thisWas', this);
            eventBus.$emit("isShowDeleteTeacherWas", true);
            eventBus.$emit("teacherIdWas", teacherId);
            eventBus.$emit("titlenameWas", `bạn có thực sự muốn xóa giáo viên có mã < ${teacherCode} > không?`);
        },
        /**
         * click btn export excel
         * CreatedBy: MinhVN(21/03/2023)
         */
        clickBtnExportExcel(){
            var m = this;
            
            axios
            .get(`${m.URLAPI}/api/v1/Teachers/GetPagingTeacher?valueFilter=${m.SearchValue}&pageIndex=${1}&pageSize=${1000}`,
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
                    const data = response.data.Data.Data
                    .map(item => [item.TeacherCode, item.FullName, m.FomatGender(item.Gender), m.FomatDate(item.DateOfBirth), item.PhoneNumber, item.Email, item.NumberCard, item.AddressCurent, item.Address]);

                    data.unshift(['Mã giáo viên', 'Tên giáo viên', 'Giới tính', 'Ngày sinh', 'Số điện thoại', 'Email', 'Số CMND/CCCD', 'Địa chỉ hiện tại', 'Quê quán']);
                    const ws = XLSX.utils.aoa_to_sheet(data);
                    ws['!cols'] = [
                        { wpx: 80 }, 
                        { wpx: 150 }, 
                        { wpx: 50 },
                        { wpx: 80 }, 
                        { wpx: 80 }, 
                        { wpx: 200 },
                        { wpx: 80 }, 
                        { wpx: 80 }, 
                        { wpx: 80 },  
                    ];
                    const wb = XLSX.utils.book_new();
                    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
                    XLSX.writeFile(wb, 'Danhsachgiaovien.xlsx');
                }
            })
            .catch(function (res){
                console.log(res);
            });
        }
    },
    created() {
        var m = this;
        m.loadDataTeacher();
    },
    
}
</script>

<style lang="css">
.active {
  background: #2ca01c;
  font-weight: bold;
  color: #fff;
}
</style>