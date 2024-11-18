import { ChangeEvent, useState, FormEvent } from 'react';
import { Button, Modal, Form, Input, Select, Result } from 'antd';
import { SmileOutlined } from '@ant-design/icons';
import './AnnouncementForm.scss';

const { TextArea } = Input;

function AnnouncementForm() {
    const [done, setDone] = useState<boolean>(true);

    const [formData, setFormData] = useState<{
        title: '',
        city: '',
        category: '',
        content: '',
    }>({
        title: '',
        city: '',
        category: '',
        content: ''
    });

    const handleChangeForm = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value
        });
    };

    const handleSubmitForm = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        console.log('Form Data:', formData);
        handleShowResult();
        // const res = await axios.post("http://localhost:8080/", formData)
        // if (res.status === 200) {
        //     console.log(res)
        //     login(res)
        // } else {
        //     console.error(res)
        // }
    };

    const handleShowResult = () => {
        setDone(true);
    }

    const handleCloseResult = () => {
        setDone(false);
    }

    if(done) return (
        <Result
            icon={<SmileOutlined />}
            title="Świetnie! Twoje ogłoszenie zostało dodane."
            extra={<Button type="primary" onClick={handleCloseResult}>Dodaj kolejne ogłoszenie</Button>}
        />
    );

    return (
            <Form layout="vertical" onSubmitCapture={handleSubmitForm} className='AddAnnouncementForm'>
                <Form.Item
                    label="Tytuł ogłoszenia"
                    name="title"
                    rules={[
                        {
                            required: true,
                            message: 'Proszę podać tytuł ogłoszenia',
                        },
                    ]}
                >
                    <Input
                        placeholder="Tytuł"
                        name="title"
                        value={formData.title}
                        onChange={handleChangeForm}
                        autoFocus
                        style={{ textAlign: 'left' }}
                    />
                </Form.Item>

                <Form.Item
                    label="Podaj miejsce ogłoszenia"
                    name="city"
                    rules={[
                        {
                            required: true,
                            message: 'Proszę podać miejsce ogłoszenia',
                        },
                    ]}
                >
                    <Input
                        placeholder="Miasto"
                        name="city"
                        value={formData.city}
                        onChange={handleChangeForm}
                        style={{ textAlign: 'left' }}
                    />
                </Form.Item>

                <Form.Item
                    label="Wybierz kategorię"
                    name="city"
                    rules={[
                        {
                            required: true,
                            message: 'Proszę podać kategorię ogłoszenia',
                        },
                    ]}
                >
                    <Select
                    style={{ width: '100%', textAlign: 'left' }}
                    allowClear
                    options={[{ value: 'Kategoria1', label: 'Pomoc domowa' }, {value: 'Kategoria2', label: 'Wsparcie psychiczne'}, {value: 'Kategoria3', label: 'Transport'}]}
                    placeholder="Wybierz kategorię"
                    />
                </Form.Item>
                
                <Form.Item
                label="Treść ogłoszenia"
                name="content"
                rules={[
                    {
                        required: true,
                        message: 'Proszę podać treść ogłoszenia',
                    },
                ]}
                >
                    <TextArea
                        placeholder="Treść ogłoszenia"
                        name="content"
                        value={formData.content}
                        onChange={handleChangeForm}
                        autoSize={{ minRows: 5, maxRows: 12 }}
                        style={{ textAlign: 'left' }}
                    />
                </Form.Item>

                <Form.Item>
                    <Button type="primary" htmlType="submit" block>
                        Dodaj ogłoszenie
                    </Button>
                </Form.Item>
            </Form>
    );
}

export default AnnouncementForm;
