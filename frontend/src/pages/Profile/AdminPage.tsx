import axios from 'axios'
import React, { useEffect, useState } from 'react'
import './style.css'
export default function AdminPage() {
  const [users, setUsers] = useState<{ id: string; username: string }[]>([])

  const getAllUser = async () => {
    try {
      const res = await axios.get('https://localhost:7749/api/AppUser')
      console.log(res.data)
      if (res.data) {
        setUsers(res.data.data)

      }
    } catch (error) {
      console.log(error)
    }
  }
  useEffect(() => {
    getAllUser()
  }, [])
  // getAllUser()
  // console.log(users);

  return (
    <div className='mt-52 '>
      <div className='relative overflow-x-auto shadow-md sm:rounded-lg'>
        <table className='w-full text-sm text-left text-gray-500 dark:text-gray-400'>
          <thead className='text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400'>
            <tr>
              <th scope='col' className='p-4'>
                <div className='flex items-center'>
                  <input
                    id='checkbox-all-search'
                    type='checkbox'
                    className='w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 rounded focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 dark:focus:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600'
                  />
                  <label htmlFor='checkbox-all-search' className='sr-only'>
                    checkbox
                  </label>
                </div>
              </th>
              <th scope='col' className='px-6 py-3'>
                ID
              </th>
              <th scope='col' className='px-6 py-3' >
                UserName
              </th>
              {/* <th scope='col' className='px-6 py-3'>
                Category
              </th>
              <th scope='col' className='px-6 py-3'>
                Price
              </th>
              <th scope='col' className='px-6 py-3'>
                Action
              </th> */}
            </tr>
          </thead>
          <tbody>
            {users.map((user, index) => (
              <>
                <tr className='bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600' >
                  <td className='w-4 p-4'>
                    <div className='flex items-center'>
                      <input
                        id='checkbox-table-search-1'
                        type='checkbox'
                        className='w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 rounded focus:ring-blue-500 dark:focus:ring-blue-600 dark:ring-offset-gray-800 dark:focus:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600'
                      />
                      <label htmlFor='checkbox-table-search-1' className='sr-only'>
                        checkbox
                      </label>
                    </div>
                  </td>
                  <td className='px-6 py-4'>{user.id}</td>
                  <td className='px-6 py-4'>{user.username}</td>
                </tr>
              </>
            ))}
          </tbody >
        </table>
        <nav className='flex items-center justify-between pt-4' aria-label='Table navigation'>
          <span className='text-sm font-normal text-gray-500 dark:text-gray-400'>
            Showing <span className='font-semibold text-gray-900 dark:text-white'>1-10</span> of{' '}
            <span className='font-semibold text-gray-900 dark:text-white'>1000</span>
          </span>
          <ul className='inline-flex items-center -space-x-px'>
            <li>
              <a
                href='#!'
                className='block px-3 py-2 ml-0 leading-tight text-gray-500 bg-white border border-gray-300 rounded-l-lg hover:bg-gray-100 hover:text-gray-700 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white'
              >
                <span className='sr-only'>Previous</span>
                <svg
                  className='w-5 h-5'
                  aria-hidden='true'
                  fill='currentColor'
                  viewBox='0 0 20 20'
                  xmlns='http://www.w3.org/2000/svg'
                >
                  <path
                    fillRule='evenodd'
                    d='M12.707 5.293a1 1 0 010 1.414L9.414 10l3.293 3.293a1 1 0 01-1.414 1.414l-4-4a1 1 0 010-1.414l4-4a1 1 0 011.414 0z'
                    clipRule='evenodd'
                  />
                </svg>
              </a>
            </li>
            <li>
              <a
                href='#!'
                className='px-3 py-2 leading-tight text-gray-500 bg-white border border-gray-300 hover:bg-gray-100 hover:text-gray-700 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white'
              >
                1
              </a>
            </li>
            <li>
              <a
                href='#!'
                className='px-3 py-2 leading-tight text-gray-500 bg-white border border-gray-300 hover:bg-gray-100 hover:text-gray-700 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white'
              >
                2
              </a>
            </li>
            <li>
              <a
                href='#!'
                aria-current='page'
                className='z-10 px-3 py-2 leading-tight text-blue-600 border border-blue-300 bg-blue-50 hover:bg-blue-100 hover:text-blue-700 dark:border-gray-700 dark:bg-gray-700 dark:text-white'
              >
                3
              </a>
            </li>
            <li>
              <a
                href='#!'
                className='px-3 py-2 leading-tight text-gray-500 bg-white border border-gray-300 hover:bg-gray-100 hover:text-gray-700 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white'
              >
                ...
              </a>
            </li>
            <li>
              <a
                href='#!'
                className='px-3 py-2 leading-tight text-gray-500 bg-white border border-gray-300 hover:bg-gray-100 hover:text-gray-700 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white'
              >
                100
              </a>
            </li>
            <li>
              <a
                href='#!'
                className='block px-3 py-2 leading-tight text-gray-500 bg-white border border-gray-300 rounded-r-lg hover:bg-gray-100 hover:text-gray-700 dark:bg-gray-800 dark:border-gray-700 dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white'
              >
                <span className='sr-only'>Next</span>
                <svg
                  className='w-5 h-5'
                  aria-hidden='true'
                  fill='currentColor'
                  viewBox='0 0 20 20'
                  xmlns='http://www.w3.org/2000/svg'
                >
                  <path
                    fillRule='evenodd'
                    d='M7.293 14.707a1 1 0 010-1.414L10.586 10 7.293 6.707a1 1 0 011.414-1.414l4 4a1 1 0 010 1.414l-4 4a1 1 0 01-1.414 0z'
                    clipRule='evenodd'
                  />
                </svg>
              </a>
            </li>
          </ul>
        </nav>
      </div>
    </div>
  )
}
