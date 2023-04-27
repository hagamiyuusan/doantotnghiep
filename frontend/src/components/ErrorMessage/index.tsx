/* eslint-disable prettier/prettier */

interface IProps {
  errorMessage: string
  typeMessage?: string
}
export const ErrorMessage = ({ errorMessage, typeMessage }: IProps) => {
  return (
    <div
      className={`mb-3 flex justify-center items-center mt-4 ${typeMessage === 'Success' ? 'text-green-700' : 'text-red-500'}`}
      style={{ minHeight: '24px' }}
    >
      {' '}
      <p>{errorMessage}</p>
    </div>
  )
}
