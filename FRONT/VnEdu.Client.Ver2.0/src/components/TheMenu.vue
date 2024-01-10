<template>
    <div class="t-menu">
        <div class="menu-top">
            <div class="top-logo"></div>
            <div class="top-name"> <span style="color: #5293C7;">vn</span><span style="color:#F2852A">Edu</span></div>
        </div>

        <!-- Quyền của admin -->
        <div v-if="user.DecentralizationName == 'Admin'" class="menu-bottom" >
            <router-link style="text-decoration: none;" to="/overview">
                <div class="bottom">
                    <div class="bottom-logo dashboard"></div>
                    <div class="bottom-name">Tổng quan</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/decentralization">
                <div class="bottom">
                    <div class="bottom-logo general-management" ></div>
                    <div class="bottom-name">Quản lý quyền</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/user">
                <div class="bottom">
                    <div class="bottom-logo student-information"></div>
                    <div class="bottom-name">Quản lý người dùng</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/changepassword">
                <div class="bottom">
                    <div class="bottom-logo forgot-password"></div>
                    <div class="bottom-name">Đổi mật khẩu</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/login">
                 <div class="bottom">
                    <div class="bottom-logo logout"></div>
                    <div class="bottom-name">Đăng xuất</div>
                </div>
            </router-link>
        </div>

        <!-- Quyền của học sinh -->
        <div v-if="user.DecentralizationName == 'Học sinh'" class="menu-bottom" >
            <router-link style="text-decoration: none;" to="/overview">
                <div class="bottom">
                    <div class="bottom-logo dashboard"></div>
                    <div class="bottom-name">Tổng quan</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/studentinformation">
                 <div class="bottom">
                    <div class="bottom-logo student-information"></div>
                    <div class="bottom-name">Thông tin cá nhân</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/scorelookup">
                <div class="bottom">
                    <div class="bottom-logo point-lookup"></div>
                    <div class="bottom-name">Tra cứu điểm</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/chat">
                <div class="bottom">
                    <div class="bottom-logo point-lookup"></div>
                    <div class="bottom-name">Hỏi đáp</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/changepassword">
                <div class="bottom">
                    <div class="bottom-logo forgot-password"></div>
                    <div class="bottom-name">Đổi mật khẩu</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/login">
                 <div class="bottom">
                    <div class="bottom-logo logout"></div>
                    <div class="bottom-name">Đăng xuất</div>
                </div>
            </router-link>
        </div>

        <!-- Quyền của giáo viên -->
        <div v-if="user.DecentralizationName == 'Giáo viên'" class="menu-bottom" >
            <router-link style="text-decoration: none;" to="/overview">
                <div class="bottom">
                    <div class="bottom-logo dashboard"></div>
                    <div class="bottom-name">Tổng quan</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/studentinformation">
                 <div class="bottom">
                    <div class="bottom-logo student-information"></div>
                    <div class="bottom-name">Thông tin cá nhân</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/detailedtablescore">
                <div class="bottom">
                    <div class="bottom-logo point-lookup"></div>
                    <div class="bottom-name">Quản lý điểm</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/changepassword">
                <div class="bottom">
                    <div class="bottom-logo forgot-password"></div>
                    <div class="bottom-name">Đổi mật khẩu</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/login">
                 <div class="bottom">
                    <div class="bottom-logo logout"></div>
                    <div class="bottom-name">Đăng xuất</div>
                </div>
            </router-link>
        </div>

        <!-- Quyền của người quản lý-->
        <div v-if="user.DecentralizationName == 'Người quản lý'" class="menu-bottom" >
            <router-link style="text-decoration: none;" to="/overview">
                <div class="bottom">
                    <div class="bottom-logo dashboard"></div>
                    <div class="bottom-name">Tổng quan</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/schoolyear">
                <div class="bottom">
                    <div class="bottom-logo general-management"></div>
                    <div class="bottom-name">Quản lý năm học</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/semester">
                <div class="bottom">
                    <div class="bottom-logo general-management"></div>
                    <div class="bottom-name">Quản lý học kì</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/class">
                <div class="bottom">
                    <div class="bottom-logo general-management"></div>
                    <div class="bottom-name">Quản lý lớp học</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/subject">
                <div class="bottom">
                    <div class="bottom-logo general-management"></div>
                    <div class="bottom-name">Quản lý môn học</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/changepassword">
                <div class="bottom">
                    <div class="bottom-logo forgot-password"></div>
                    <div class="bottom-name">Đổi mật khẩu</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/login">
                 <div class="bottom">
                    <div class="bottom-logo logout"></div>
                    <div class="bottom-name">Đăng xuất</div>
                </div>
            </router-link>
        </div>

        <!-- Quyền của nhân viên-->
        <div v-if="user.DecentralizationName == 'Nhân viên'" class="menu-bottom" >
            <router-link style="text-decoration: none;" to="/overview">
                <div class="bottom">
                    <div class="bottom-logo dashboard"></div>
                    <div class="bottom-name">Tổng quan</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/student">
                <div class="bottom">
                    <div class="bottom-logo student-information"></div>
                    <div class="bottom-name">Quản lý học sinh</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/teacher">
                <div class="bottom">
                    <div class="bottom-logo student-information"></div>
                    <div class="bottom-name">Quản lý giáo viên</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/studentclass">
                <div class="bottom">
                    <div class="bottom-logo general-management"></div>
                    <div class="bottom-name">Phân lớp cho học sinh</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/studentsubject">
                <div class="bottom">
                    <div class="bottom-logo general-management"></div>
                    <div class="bottom-name">Phân môn cho học sinh</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/teacherclass">
                <div class="bottom">
                    <div class="bottom-logo general-management"></div>
                    <div class="bottom-name">Phân lớp cho giáo viên</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/teachersubject">
                <div class="bottom">
                    <div class="bottom-logo general-management"></div>
                    <div class="bottom-name">Phân môn cho giáo viên</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/changepassword">
                <div class="bottom">
                    <div class="bottom-logo forgot-password"></div>
                    <div class="bottom-name">Đổi mật khẩu</div>
                </div>
            </router-link>
            <router-link style="text-decoration: none;" to="/login">
                 <div class="bottom">
                    <div class="bottom-logo logout"></div>
                    <div class="bottom-name">Đăng xuất</div>
                </div>
            </router-link>
        </div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
    data() {
        return {
            isShowBoxOptionGeneral: false,
        }
    },
    computed: {
        ...mapGetters(['user']),
    },
    methods: {
        /**
         * click vào quản lý chung
         * CreatedBy: MinhVN(27/01/2022)
         */
        clickBtnGeneral(){
            this.isShowBoxOptionGeneral = !this.isShowBoxOptionGeneral;
        }
    },
}
</script>