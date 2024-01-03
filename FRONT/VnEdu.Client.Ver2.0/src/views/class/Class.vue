<template>
    <div class="t-schoolyear">
        <div class="header-schoolyear">
            <div class="header-name">Thông tin lớp học</div>
            <div class="header-btn-insert" @click="clickBtnInsert()">Thêm lớp học mới</div>
        </div>
        <div class="box-search box-excel">
            <div class="box-input-text">
                <input type="text" class="input-text" placeholder="Tìm kiếm theo tên, năm học" v-model="SearchValue">
                <div class="box-icon-search" @click="clickBtnSearch()">
                    <div class="icon-search"></div>
                </div>
            </div>
            <div class="export-excel" @click="clickBtnExportExcel()"></div>
        </div>
        <div class="between-schoolyear table-user">
            <table class="m-table" border="1">
                <thead>
                    <tr>
                        <th class="text-align-center" style="width: 100px">STT</th>
                        <th class="text-align-center">Tên lớp học</th>
                        <th class="text-align-center">Giáo viên chủ nhiệm</th>
                        <th class="text-align-center">Khối học</th>
                        <th class="text-align-center">Phòng học</th>
                        <th class="text-align-center">Tên năm học</th>
                        <th class="text-align-center" style="width: 150px">Chức năng</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(Class,i) in Classs" :key="Class.ClassId">
                        <th class="text-align-center">{{++i}}</th>
                        <th class="text-align-center">{{Class.ClassName}}</th>
                        <th class="text-align-center">{{Class.FullName}}</th>
                        <th class="text-align-center">{{Class.Grade}}</th>
                        <th class="text-align-center">{{Class.Room}}</th>
                        <th class="text-align-center">{{Class.SchoolYearName}}</th>
                        <th class="text-align-center">
                            <div class="box-function">
                                <div class="btn-edit" @click="clickBtnEdit(Class.ClassId)"></div>
                                <div class="btn-delete" @click="clickBtnDelete(Class.ClassId, Class.ClassName, Class.SchoolYearName)"></div>
                            </div>
                        </th>
                    </tr>  
                </tbody>
            </table>
        </div>
        <div class="bottom-schoolyear box-type-paging">
            <div class="box-sum">Tổng số: {{Classs.length}} bản ghi</div>
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
        <class-infor></class-infor>
        <delete-class></delete-class>
    </div>
</template>
<script>
import * as XLSX from 'xlsx'
import axios from 'axios'
import { eventBus } from '../../main'
import ClassInfor from './ClassInfor.vue'
import DeleteClass from './DeleteClass.vue'
import { mapGetters } from 'vuex'
export default {
  components: { ClassInfor, DeleteClass },
    data() {
        return {
            Classs: [],
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
            pagename: "10 bản ghi trên 1 trang",
            isShowBoxItemSize: false,
            SearchValue: '',
            pageSize: 10, 
            pageNumber: 1,
            totalRecord: null,
            totalPage: null,
        }
    },
    computed: {
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
            m.loadDataClass();
        },
        clickPageNext(){
            var m = this;
            if(m.pageNumber < m.totalPage)
            {
                m.pageNumber++;
                m.loadDataClass();
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
                m.loadDataClass();
                console.log(m.pageNumber);
            }
            else
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Đã đến trang đầu tiên");
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
         * click vào chọn số bản ghi trên 1 trang
         * CreatedBy: MinhVN(08/02/2022)
         */
        clickItemSize(index, name){
            this.isShowBoxItemSize = !this.isShowBoxItemSize;
            this.pagename = name;
            this.pageSize = index;
            this.loadDataClass();
        },
        /**
         * click vào btn thêm mới lớp học
         * CreatedBy: MinhVN(08/02/2022)
         */
        clickBtnInsert(){
            eventBus.$emit("isShowBtnSaveAndInsertWas", true);
            eventBus.$emit("isShowClassInforWas", true);
            eventBus.$emit('formodWas', "add");
            eventBus.$emit('thisWas', this);
            eventBus.$emit('classWas', {});
        },
        /**
         * Lấy tất cả người dùng
         * CreatedBy: MinhVN(08/02/2022)
         */
        loadDataClass(){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Classs/GetPagingClass?valueFilter=${m.SearchValue}&pageIndex=${m.pageNumber}&pageSize=${m.pageSize}`, 
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
                    m.Classs = response.data.Data.Data;
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
        clickBtnEdit(classId){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Classs/${classId}`,
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
                    eventBus.$emit('classWas', response.data.Data);
                } 
            })
            .catch(function (res){
                console.log(res);
            });

            eventBus.$emit("isShowClassInforWas", true);
            eventBus.$emit("isShowBtnSaveAndInsertWas", false);
            eventBus.$emit('formodWas', "edit");
            eventBus.$emit('thisWas', this);
        },
        /**
         * click btn xóa
         * CreatedBy: MinhVN(08/02/2022)
         */
        clickBtnDelete(classId, className, schoolYearName){
            eventBus.$emit('thisWas', this);
            eventBus.$emit("isShowDeleteClassWas", true);
            eventBus.$emit("classIdWas", classId);
            eventBus.$emit("titlenameWas", `bạn có thực sự muốn xóa < ${className} > của < ${schoolYearName} > không?`);
        },
        /**
         * click vào btn tìm kiếm 
         * CreatedBy: MinhVN (07/01/2023)
         */
        clickBtnSearch(){
            this.pageNumber = 1;
            this.loadDataClass();
            eventBus.$emit("isShowToastMessageWas", true);
            eventBus.$emit("TitleToastMessageWas", "Tìm kiếm lớp học thành công");
        },

        /**
         * click btn export excel
         * CreatedBy: MinhVN(21/03/2023)
         */
        clickBtnExportExcel(){
            var m = this;
            
            axios
            .get(`${m.URLAPI}/api/v1/Classs/GetPagingClass?&pageIndex=${1}&pageSize=${1000}`, 
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
                    .map(item => [item.ClassName, item.FullName, item.Grade, item.Room, item.SchoolYearName]);

                    data.unshift(['Tên lớp học', 'Giáo viên chủ nhiệm', 'Khối học', 'Phòng học', 'Tên năm học']);

                    const ws = XLSX.utils.aoa_to_sheet(data);
                    ws['!cols'] = [
                        { wpx: 80 }, 
                        { wpx: 150 }, 
                        { wpx: 50 },
                        { wpx: 60 }, 
                        { wpx: 110 }, 
                    ];
                    const wb = XLSX.utils.book_new();
                    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
                    XLSX.writeFile(wb, 'Danhsachlophoc.xlsx');
                }
            })
            .catch(function (res){
                console.log(res);
            });
        }
    },
    created() {
        var m = this;
        m.loadDataClass();
    },
    
}
</script>