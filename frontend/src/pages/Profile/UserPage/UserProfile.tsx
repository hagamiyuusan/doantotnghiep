import React, { useContext,useState } from 'react'
import img from 'src/imgs/default_avatar.svg'
import { AppContext } from 'src/Context/context'
import axios from 'axios';
import { log } from 'console';

export default function UserProfile() {
  const { profile } = useContext(AppContext)
  const [currentPassword, setCurrentPassword] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const regex = /^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{8,}$/
  const handleSubmit = async (e: { preventDefault: () => void; }) => {
    e.preventDefault();
    if (!regex.test(newPassword)) {
      setError('Password must contain at least 1 uppercase letter, 1 number and 1 special character ')
      return
    }
    if (newPassword !== confirmPassword) {
      setError('Mật khẩu mới và xác nhận mật khẩu không khớp.')
      return;
    }

    try {
      // Gửi yêu cầu HTTP đến API để thay đổi mật khẩu
      const res = await axios.post('https://localhost:7749/api/UserService/changepassword', {
        userName: profile?.userName,
        currentPassword,
        newPassword,
        confirmPassword
      });
      if (res.status === 200) {
        setTimeout(() => {
          window.open('https://localhost:3000')

        }, 2000)
      }
      if (res.status === 400) {
        setError('Mật khẩu hiện tại chưa đúng.');
        return;
      }
      console.log(res.status)

      // Xử lý phản hồi từ API (nếu cần)      
      // Đặt lại trạng thái biểu mẫu
      setCurrentPassword('');
      setNewPassword('');
      setConfirmPassword('');
      setError('');
    } catch (error) {
      // Xử lý lỗi từ API (nếu có)
      setError('Bạn phải điền đầy đủ thông tin');
      console.log(error);
    }
  };

  return (
    <div>
      <div className=' mt-32 grid grid-flow-col grid-cols-3 h-[600px]'>
        <div className=' col-span-1 text-cyan-300 pt-6'>
          <div className='USEr_INFO flex justify-center gap-3'>
            <div className='  w-16 h-16 '>
              <img src={img} alt='' className=' rounded-full w-full h-full' />
            </div>
            <div className=' text-red-600 pt-3 mt-3 font-bold text-base '>{profile?.userName}</div>
          </div>
        </div>
        <div className=' col-span-2 text-white bg-slate-900 px-7 pt-7 rounded'>
          <div className=' border-b border-gray-300 mb-7 pb-4'>
            <h1 className=' text-3xl uppercase mb-1'>My Profile</h1>
            <h6 className=' text-lg text-gray-300'>Manage profile information for account security</h6>
          </div>
          <div className=''>
            <form action='' className=' px-3' onSubmit={handleSubmit}>
              <div className='flex items-center gap-6 mb-7'>
                <label htmlFor='username' className='text-sm m-0  '>
                  UserName
                </label>
                <span id='username' className='text-sm p-y-3  inline-block ml-[30px]'>
                  {profile?.userName}
                </span>
              </div>
              <div className='flex items-center gap-6 mb-7'>
                <label className='text-sm m-0 w-24' htmlFor='current-password'>
                 Current Password 
                </label>
                <input id='current-password' type='password' 
                  value={currentPassword}
                  onChange={(e) => {setCurrentPassword(e.target.value), setError('')}}
                className='p-3 bg-white text-black'  required/>
              </div>
              <div className='flex items-center gap-6 mb-7'>
                <label className='text-sm m-0 w-24' htmlFor='new-password' >
                New Paswword  
                </label>
                <input id='new-password' type='password' 
                  value={newPassword} required
                  onChange={(e) => {setNewPassword(e.target.value), setError('')}}
                className='p-3 bg-white text-black'  />
              </div>
              <div className='flex items-center gap-6 mb-7'>
                <label className='text-sm m-0 w-24' htmlFor='confirm-password'>
                Confirm Password 
                </label>
                <input id='confirm-password' type='password' 
                  value={confirmPassword} required
                  onChange={(e) => {setConfirmPassword(e.target.value), setError('')}}
                className='p-3 bg-white text-black' />
              </div>
              <div className='flex items-center gap-6 mb-7'>
                <label className='text-sm m-0' htmlFor='email'>
                  Email
                </label>
                <span id='email' className='text-sm inline-block ml-[60px] '>
                  {profile?.email}
                </span>
              </div>
              {error && <div className="error text-center text-red-600">{error}</div>}
  <div className="flex justify-center items-center">
  <button type='submit' className='bg-red-600 mt-10 pr-2 pl-2 ' >Update Profile</button>

  </div>            
  </form>
          </div>
        </div>
      </div>
    </div>
  )
}
