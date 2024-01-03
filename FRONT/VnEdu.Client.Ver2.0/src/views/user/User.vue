<template>
    <div class="t-schoolyear">
        <div class="header-schoolyear">
            <div class="header-name">Thông tin người dùng</div>
            <div class="header-btn-insert" @click="clickBtnInsert()">Thêm người dùng mới</div>
        </div>
        <div class="box-search box-excel">
            <div class="box-input-text">
                <input type="text" class="input-text" placeholder="Tìm kiếm theo tên, số điện thoại" v-model="SearchValue">
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
                        <th class="text-align-center">Tên người dùng</th>
                        <th class="text-align-center">Số điện thoại</th>
                        <th class="text-align-center">Email</th>
                        <th class="text-align-center">Tên quyền</th>
                        <th class="text-align-center" style="width: 150px">Chức năng</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(user,i) in users" :key="user.UserId">
                        <th class="text-align-center">{{++i}}</th>
                        <th class="text-align-center">{{user.UserName}}</th>
                        <th class="text-align-center">{{user.PhoneNumber}}</th>
                        <th class="text-align-center">{{user.Email}}</th>
                        <th class="text-align-center">{{user.DecentralizationName}}</th>
                        <th class="text-align-center">
                            <div class="box-function">
                                <div class="btn-edit" @click="clickBtnEdit(user.UserId)"></div>
                                <div class="btn-delete" @click="clickBtnDelete(user.UserId, user.UserName)"></div>
                            </div>
                        </th>
                    </tr>  
                </tbody>
            </table>
        </div>
        <div class="bottom-schoolyear box-type-paging">
            <div class="box-sum">Tổng số: {{users.length}} bản ghi</div>
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
        <user-infor></user-infor>
        <delete-user></delete-user>
    </div>
</template>
<script>
import * as XLSX from 'xlsx'
import axios from 'axios'
import { eventBus } from '../../main'
import UserInfor from './UserInfor.vue'
import DeleteUser from './DeleteUser.vue'
import { mapGetters } from 'vuex'
export default {
  components: { UserInfor, DeleteUser },
    data() {
        return {
            users: [],
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
            m.loadDataAdministration();
        },
        clickPageNext(){
            var m = this;
            if(m.pageNumber < m.totalPage)
            {
                m.pageNumber++;
                m.loadDataAdministration();
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
                m.loadDataAdministration();
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
            this.loadDataAdministration();
            eventBus.$emit("isShowToastMessageWas", true);
            eventBus.$emit("TitleToastMessageWas", "Tìm kiếm người dùng thành công");
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
         /**
         * click vào btn item page 
         * CreatedBy: MinhVN (05/01/2021)
         */
        clickItemSize(index, name){
            this.pageSize = index;
            this.pagename = name;
            this.loadDataAdministration();
            this.isShowBoxItemSize = false;
        },
        /**
         * click vào btn thêm mới người dùng
         * CreatedBy: MinhVN(08/02/2022)
         */
        clickBtnInsert(){
            eventBus.$emit("isShowBtnSaveAndInsertWas", true);
            eventBus.$emit("isShowAdministrationInforWas", true);
            eventBus.$emit('formodWas', "add");
            eventBus.$emit('thisWas', this);
            eventBus.$emit('administrationWas', {});
        },
        /**
         * Lấy tất cả người dùng
         * CreatedBy: MinhVN(08/02/2022)
         */
        loadDataAdministration(){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Users/GetPagingUser?valueFilter=${m.SearchValue}&pageIndex=${m.pageNumber}&pageSize=${m.pageSize}`,
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
                    m.users = response.data.Data.Data;
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
        clickBtnEdit(userId){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Users/${userId}`,
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
                    eventBus.$emit('administrationWas', response.data.Data);
                } 
            })
            .catch(function (res){
                console.log("api lỗi" + res);
            });
            eventBus.$emit("isShowAdministrationInforWas", true);
            eventBus.$emit("isShowBtnSaveAndInsertWas", false);
            eventBus.$emit('formodWas', "edit");
            eventBus.$emit('thisWas', this);
        },
        /**
         * click btn xóa
         * CreatedBy: MinhVN(08/02/2022)
         */
        clickBtnDelete(userId, userName){
            eventBus.$emit('thisWas', this);
            eventBus.$emit("isShowDeleteAdministrationWas", true);
            eventBus.$emit("administrationIdWas", userId);
            eventBus.$emit("titlenameWas", `Bạn có thực sự muốn xóa người dùng < ${userName} > không?`);
        },

        /**
         * click btn export excel
         * CreatedBy: MinhVN(21/03/2023)
         */
        clickBtnExportExcel(){
            var m = this;
            
            axios
            .get(`${m.URLAPI}/api/v1/Users/GetPagingUser?pageIndex=${1}&pageSize=${1000}`,
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
                    .map(item => [item.UserName, item.PhoneNumber, item.Email, item.DecentralizationName]);

                    data.unshift(['Tên người dùng', 'Số điện thoại', 'Email', 'Tên quyền']);

                    const ws = XLSX.utils.aoa_to_sheet(data);
                    ws['!cols'] = [
                        { wpx: 150 }, 
                        { wpx: 80 }, 
                        { wpx: 200 },
                        { wpx: 80 }
                    ];
                    const wb = XLSX.utils.book_new();
                    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
                    XLSX.writeFile(wb, 'Danhsachnguoidung.xlsx');
                }
            })
            .catch(function (res){
                console.log(res);
            });
        }
    },
    created() {
        var m = this;
        m.loadDataAdministration();
    },
    
}
</script>