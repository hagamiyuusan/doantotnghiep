import './style.css'
import img from 'src/imgs/default_avatar.svg'
export default function UserProfile() {
  return (
    <div className='container'>
      <div className=' mt-32 grid grid-flow-col grid-cols-3 h-[600px]'>
        <div className=' col-span-1 text-cyan-300 pt-6'>
          <div className='USEr_INFO flex justify-center gap-3'>
            <div className='  w-16 h-16 '>
              <img src={img} alt='' className=' rounded-full w-full h-full' />
            </div>
            <div className=' text-gray-50 pt-3 text-xs '>USERNAME</div>
          </div>
        </div>
        <div className=' col-span-2 text-white bg-slate-900 px-7 pt-7 rounded'>
          <div className=' border-b border-gray-300 mb-7 pb-4'>
            <h1 className=' text-3xl uppercase mb-1'>My Profile</h1>
            <h6 className=' text-lg text-gray-300'>Manage profile information for account security</h6>
          </div>
          <div className=''>
            <form action='' className=' px-3'>
              <div className='flex items-center gap-6 mb-7'>
                <label htmlFor='username' className='text-sm m-0 w-24'>
                  UserName
                </label>
                <span id='username' className='text-sm p-y-3'>
                  USERNAME
                </span>
              </div>
              <div className='flex items-center gap-6 mb-7'>
                <label className='text-sm m-0 w-24' htmlFor='name'>
                  Name
                </label>
                <input id='name' type='text' className='p-3 bg-white text-black' value='username' />
              </div>
              <div className='flex items-center gap-6 mb-7'>
                <label className='text-sm m-0 w-24' htmlFor='name'>
                  Address
                </label>
                <input id='name' type='text' className='p-3 bg-white text-black' value='username' />
              </div>
              <div className='flex items-center gap-6 mb-7'>
                <label className='text-sm m-0 w-24' htmlFor='email'>
                  Email
                </label>
                <span id='email' className='text-sm'>
                  Email
                </span>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  )
}
